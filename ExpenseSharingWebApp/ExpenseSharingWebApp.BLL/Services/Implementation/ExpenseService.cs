using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using ExpenseSharingWebApp.DAL.Repositories.Implementation;
using ExpenseSharingWebApp.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.BLL.Services.Implementation
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ExpenseService(IExpenseRepository expenseRepository, IGroupRepository groupRepository,IMapper mapper, IUserRepository userRepository)
        {
            this._expenseRepository = expenseRepository;
            this._groupRepository = groupRepository;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        public async Task<ExpenseResponseDto> CreateExpenseAsync(CreateExpenseRequestDto createExpenseRequestDto)
        {
            var expense = _mapper.Map<Expense>(createExpenseRequestDto);
            expense.Id = Guid.NewGuid().ToString();
            expense.Date = DateTime.UtcNow;
            expense.IsSettled = false;

            var group = await _groupRepository.GetGroupByIdAsync(createExpenseRequestDto.GroupId);
            if (group == null)
            {
                throw new Exception("Group not found.");
            }

            var paidByUser = await _groupRepository.GetUserByIdAsync(createExpenseRequestDto.PaidByUserId);
            if (paidByUser == null)
            {
                throw new Exception("User who paid not found.");
            }
            expense.SplitAmong = new List<User>();
            foreach (var userId in createExpenseRequestDto.SplitWithUserIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                    expense.SplitAmong.Add(user);
                }
                else
                {
                    throw new Exception($"User with ID {userId} not found.");
                }
            }
            decimal individualShare;
            if(expense.SplitAmong.Count == 1)
            {
                individualShare = expense.Amount / 2;
            }
            else
            {
                individualShare = expense.Amount / expense.SplitAmong.Count; //got exception over this line
            }

            expense.ExpenseSplits = createExpenseRequestDto.SplitWithUserIds.Select(userId => new ExpenseSplit
            {
                UserId = userId,
                PaidToUserId = createExpenseRequestDto.PaidByUserId,
                ExpenseId = expense.Id,
                AmountOwed = userId == createExpenseRequestDto.PaidByUserId ? 0 : individualShare,
                AmountPaid = userId == createExpenseRequestDto.PaidByUserId ? expense.Amount : 0,
                IsSettled = false

            }).ToList();
            // Add an ExpenseSplit for the PaidByUserId if not already added
            if (!expense.ExpenseSplits.Any(es => es.UserId == createExpenseRequestDto.PaidByUserId))
            {
                expense.ExpenseSplits.Add(new ExpenseSplit
                {
                    UserId = createExpenseRequestDto.PaidByUserId,
                    PaidToUserId = createExpenseRequestDto.PaidByUserId,
                    ExpenseId = expense.Id,
                    AmountOwed = 0,
                    AmountPaid = expense.Amount,
                    IsSettled = false
                });
            }


            await _expenseRepository.CreateExpenseAsync(expense);
            return _mapper.Map<ExpenseResponseDto>(expense);

            // Update balances for each user
            //foreach (var userId in createExpenseRequestDto.SplitWithUserIds)
            //{
            //    if (userId == paidByUser.Id) continue;
            //    await UpdateUserBalanceAsync(userId, expense.GroupId, individualShare);

            //    //var userBalance = await _expenseRepository.GetUserBalanceAsync(userId, expense.GroupId) ?? new UserBalance { Id = Guid.NewGuid().ToString(), UserId = userId, GroupId = expense.GroupId, AmountOwed = 0 };
            //    //userBalance.AmountOwed += individualShare;
            //    //await _expenseRepository.UpdateUserBalanceAsync(userBalance); //exception here
            //}

            // Update balance for the payer
            //await UpdateUserBalanceAsync(createExpenseRequestDto.PaidByUserId, expense.GroupId, -(expense.Amount - individualShare));

            //var createdExpense = await _expenseRepository.CreateExpenseAsync(expense);
            //return _mapper.Map<ExpenseResponseDto>(createdExpense);
            //// Update balance for the payer
            //var payerBalance = await _expenseRepository.GetUserBalanceAsync(createExpenseRequestDto.PaidByUserId, expense.GroupId) ?? new UserBalance { Id = Guid.NewGuid().ToString(), UserId = createExpenseRequestDto.PaidByUserId, GroupId = expense.GroupId, AmountOwed = 0 };
            //payerBalance.AmountOwed -= expense.Amount - individualShare;
            //await _expenseRepository.UpdateUserBalanceAsync(payerBalance);

            //var createdExpense = await _expenseRepository.CreateExpenseAsync(expense);
            //return _mapper.Map<ExpenseResponseDto>(createdExpense);
        }

        private async Task UpdateUserBalanceAsync(string userId, string groupId, decimal amount)
        {
            for (int i = 0; i < 3; i++) // Retry logic: try up to 3 times
            {
                try
                {
                    var userBalance = await _expenseRepository.GetUserBalanceAsync(userId, groupId);
                    if (userBalance == null)
                    {
                        userBalance = new UserBalance { Id = Guid.NewGuid().ToString(), UserId = userId, GroupId = groupId, AmountOwed = amount };
                        await _expenseRepository.CreateUserBalanceAsync(userBalance);
                    }
                    else
                    {
                        userBalance.AmountOwed += amount;
                        await _expenseRepository.UpdateUserBalanceAsync(userBalance);
                    }
                    break; // Break out of the loop if update is successful
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (i == 2) // If it fails 3 times, rethrow the exception
                    {
                        throw;
                    }
                    // Reload user balance from the database
                    var userBalance = await _expenseRepository.GetUserBalanceAsync(userId, groupId);
                    if (userBalance == null)
                    {
                        userBalance = new UserBalance { Id = Guid.NewGuid().ToString(), UserId = userId, GroupId = groupId, AmountOwed = amount };
                        await _expenseRepository.CreateUserBalanceAsync(userBalance);
                    }
                }
            }
        }

            public async Task<ExpenseResponseDto> GetExpenseByIdAsync(string expenseId)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);
            if(expense == null)
            {
                return null;
            }
            var paidByUser = await _userRepository.GetUserByIdAsync(expense.PaidByUserId);
            var expenseResponseDto = new ExpenseResponseDto
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                PaidByUser = paidByUser == null ? null : new UserDto
                {
                    Id = paidByUser.Id,
                    Email = paidByUser.Email,
                    Name = paidByUser.Name
                },
                IsSettled = expense.IsSettled,
                ExpenseSplits = await Task.WhenAll(expense.ExpenseSplits.Select(async split =>
                {
                    var owedUser = await _userRepository.GetUserByIdAsync(split.UserId);
                    return new ExpenseSplitResponeDto
                    {
                        ExpenseId = split.ExpenseId,
                        Description = expense.Description,
                        Amount = expense.Amount,
                        Date = expense.Date,
                        PaidByUserId = expense.PaidByUserId,
                        UserShare = split.AmountOwed,
                        AmountPaid = split.AmountPaid,
                        AmountOwed = split.AmountOwed,
                        OwedUser = owedUser == null ? null : new UserDto
                        {
                            Id = owedUser.Id,
                            Email = owedUser.Email,
                            Name = owedUser.Name
                        }
                    };
                }).ToList())

            //ExpenseSplits = expense.ExpenseSplits.Select(split => new ExpenseSplitResponeDto
            //{

            //    ExpenseId = split.ExpenseId,
            //    Description = expense.Description,
            //    Amount = expense.Amount,
            //    Date = expense.Date,
            //    PaidByUserId = expense.PaidByUserId,
            //    UserShare = split.AmountOwed,
            //    AmountPaid = split.AmountPaid,
            //    AmountOwed = split.AmountOwed
            //}).ToList()
        };
            return expenseResponseDto;
            //return _mapper.Map<ExpenseResponseDto>(expense);
        }

        public async Task<List<ExpenseResponseDto>> GetAllExpensesByGroupIdAsync(string groupId)
        {
            var expenses = await _expenseRepository.GetAllExpensesByGroupIdAsync(groupId);
            if(expenses == null)
            {
                return null;
            }
            return _mapper.Map<List<ExpenseResponseDto>>(expenses);
        }

        public async Task DeleteExpenseAsync(string expenseId)
        {
            await _expenseRepository.DeleteExpenseAsync(expenseId);  
        }
        public async Task UpdateExpenseAsync(string expenseId, UpdateExpenseRequestDto updateExpenseRequestDto)
        {
            var expenseToBeUpdate = await _expenseRepository.GetExpenseByIdAsync(expenseId);
            if (expenseToBeUpdate == null)
            {
                throw new Exception("Expense Not Found!");
            }

            _mapper.Map(updateExpenseRequestDto, expenseToBeUpdate);

            // Re-calculate the expense splits
            expenseToBeUpdate.ExpenseSplits.Clear();

            // Determine total amount paid by the user who paid for the expense
            decimal totalAmountPaid = updateExpenseRequestDto.Amount;

            // Calculate individual share for each user who owes
            decimal individualShare = totalAmountPaid / (updateExpenseRequestDto.SplitWithUserIds.Count + 1);

            // Add the paidByUser's split
            expenseToBeUpdate.ExpenseSplits.Add(new ExpenseSplit
            {
                UserId = updateExpenseRequestDto.PaidByUserId,
                PaidToUserId = null,
                AmountPaid = totalAmountPaid,
                AmountOwed = 0
            });

            // Add splits for users who owe
            foreach (var userId in updateExpenseRequestDto.SplitWithUserIds)
            {
                if (userId != updateExpenseRequestDto.PaidByUserId)
                {
                    expenseToBeUpdate.ExpenseSplits.Add(new ExpenseSplit
                    {
                        UserId = userId,
                        PaidToUserId = updateExpenseRequestDto.PaidByUserId,
                        AmountPaid = 0,
                        AmountOwed = individualShare
                    });
                }
            }

            await _expenseRepository.UpdateExpenseAsync(expenseToBeUpdate);
        }


        //public async Task UpdateExpenseAsync(string expenseId, UpdateExpenseRequestDto updateExpenseRequestDto)
        //{
        //    var expenseToBeUpdate = await _expenseRepository.GetExpenseByIdAsync(expenseId);
        //    if(expenseToBeUpdate == null) 
        //    {
        //        throw new Exception("Expense Not Found!");
        //    }

        //    _mapper.Map(updateExpenseRequestDto, expenseToBeUpdate);

        //    // Re-calculate the expense splits
        //    expenseToBeUpdate.ExpenseSplits.Clear();
        //    decimal individualShare = updateExpenseRequestDto.Amount / updateExpenseRequestDto.SplitWithUserIds.Count;
        //    foreach (var userId in updateExpenseRequestDto.SplitWithUserIds)
        //    {
        //        var user = await _groupRepository.GetUserByIdAsync(userId);
        //        if (user != null)
        //        {
        //            if (user.Id == updateExpenseRequestDto.PaidByUserId)
        //            {
        //                expenseToBeUpdate.ExpenseSplits.Add(new ExpenseSplit
        //                {
        //                    UserId = user.Id,
        //                    PaidToUserId = null,
        //                    AmountPaid = updateExpenseRequestDto.Amount,
        //                    AmountOwed = 0
        //                });
        //            }
        //            else
        //            {
        //                expenseToBeUpdate.ExpenseSplits.Add(new ExpenseSplit
        //                {
        //                    UserId = user.Id,
        //                    PaidToUserId = updateExpenseRequestDto.PaidByUserId,
        //                    AmountPaid = 0,
        //                    AmountOwed = individualShare
        //                });
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception($"User with ID {userId} not found.");
        //        }

        //    }

        //    await _expenseRepository.UpdateExpenseAsync(expenseToBeUpdate);
        //}

        public async Task SettleExpenseAsync(SettleExpenseRequestDto settleExpenseDto)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(settleExpenseDto.ExpenseId);
            if (expense == null)
            {
                throw new Exception("Expense not found.");
            }

            var split = expense.ExpenseSplits.FirstOrDefault(es => es.UserId == settleExpenseDto.SettledByUserId);
            if (split == null)
            {
                throw new Exception("No expense split found for this user.");
            }

            if (split.IsSettled)
            {
                throw new Exception("User's portion of the expense is already settled.");
            }

            split.IsSettled = true;

            var userBalance = await _expenseRepository.GetUserBalanceAsync(settleExpenseDto.SettledByUserId, expense.GroupId);
            userBalance.AmountOwed -= split.AmountOwed;

            if (userBalance.AmountOwed == 0)
            {
                userBalance.IsSettled = true;
            }

            await _expenseRepository.UpdateUserBalanceAsync(userBalance);

            var payerBalance = await _expenseRepository.GetUserBalanceAsync(split.PaidToUserId, expense.GroupId);
            payerBalance.AmountOwed += split.AmountOwed;

            await _expenseRepository.UpdateUserBalanceAsync(payerBalance);

            if (expense.ExpenseSplits.Where(es => es.UserId != split.PaidToUserId).All(es => es.IsSettled))
            {
                expense.IsSettled = true;
            }

            await _expenseRepository.UpdateExpenseAsync(expense);

            //if (expense.IsSettled)
            //{
            //    throw new Exception("Expense is already settled.");
            //}

            //expense.IsSettled = true;

            //foreach (var split in expense.ExpenseSplits)
            //{
            //    var userBalance = await _expenseRepository.GetUserBalanceAsync(split.UserId, expense.GroupId);
            //    userBalance.AmountOwed -= split.Amount;

            //    if (userBalance.AmountOwed == 0)
            //    {
            //        userBalance.IsSettled = true;
            //    }

            //    await _expenseRepository.UpdateUserBalanceAsync(userBalance);
            //}

            //await _expenseRepository.UpdateExpenseAsync(expense);
        }

        public async Task<Dictionary<string, decimal>> GetGroupBalancesAsync(string groupId)
        {
            var expenses = await _expenseRepository.GetAllExpensesByGroupIdAsync(groupId);
            var balances = new Dictionary<string, decimal>();

            foreach (var expense in expenses)
            {
                foreach (var split in expense.ExpenseSplits)
                {
                    if (!balances.ContainsKey(split.UserId))
                    {
                        balances[split.UserId] = 0;
                    }
                    if (!balances.ContainsKey(split.PaidToUserId))
                    {
                        balances[split.PaidToUserId] = 0;
                    }
                    //update userBalance for user who owed money
                    if (split.UserId == expense.PaidByUserId)
                    {
                        balances[split.UserId] += split.AmountOwed;
                    }
                    else
                    {
                        balances[split.UserId] -= split.AmountPaid;
                    }
                }
            }

            return balances;
        }

        public async Task<List<ExpenseResponseDto>> GetUserExpensesAsync(string groupId, string userId)
        {
            var expenses = await _expenseRepository.GetUserExpensesAsync(groupId, userId);
            return _mapper.Map<List<ExpenseResponseDto>>(expenses);
        }
        public async Task<IEnumerable<ExpenseSplitResponeDto>> GetExpenseSplitsAsync(string userId, string groupId)
        {
            var expenseSplits = await _expenseRepository.GetExpenseSplitsAsync(userId, groupId);
            return _mapper.Map<IEnumerable<ExpenseSplitResponeDto>>(expenseSplits);
        }
    }
}
