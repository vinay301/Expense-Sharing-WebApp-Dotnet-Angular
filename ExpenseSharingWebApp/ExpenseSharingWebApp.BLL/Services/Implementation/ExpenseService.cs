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

            // Initialize the ExpenseSplits list
            expense.ExpenseSplits = new List<ExpenseSplit>();

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

            decimal individualShare;
            if (createExpenseRequestDto.SplitWithUserIds.Count == 1)
            {
                individualShare = expense.Amount / 2;
            }
            else
            {
                individualShare = expense.Amount / (createExpenseRequestDto.SplitWithUserIds.Count + 1);
            }

            foreach (var userId in createExpenseRequestDto.SplitWithUserIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    throw new Exception($"User with ID {userId} not found.");
                }


                var amountOwed = userId == createExpenseRequestDto.PaidByUserId ? 0 : individualShare;
                var amountPaid = userId == createExpenseRequestDto.PaidByUserId ? expense.Amount : 0;

                var split = new ExpenseSplit
                {
                    UserId = userId,
                    PaidToUserId = createExpenseRequestDto.PaidByUserId,
                    ExpenseId = expense.Id,
                    AmountOwed = amountOwed,
                    AmountPaid = amountPaid,
                    IsSettled = false
                };

                expense.ExpenseSplits.Add(split);

                // Check if the user balance exists, if not create a new one
                var userBalance = await _expenseRepository.GetUserBalanceAsync(userId, createExpenseRequestDto.GroupId);
                if (userBalance == null)
                {
                    userBalance = new UserBalance
                    {
                        //Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        GroupId = createExpenseRequestDto.GroupId,
                        AmountOwed = 0,
                        AmountPaid = 0,
                        IsSettled = false
                    };
                }

               
                userBalance.AmountOwed += amountOwed;
                userBalance.AmountPaid += amountPaid;

                await _expenseRepository.UpdateUserBalanceAsync(userBalance);
            }

            await _expenseRepository.CreateExpenseAsync(expense);
            return _mapper.Map<ExpenseResponseDto>(expense);
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
                IsSettled = expense.IsSettled, //this value is false
                GroupId = expense.GroupId,
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
                        IsSettled = split.IsSettled, //this is true, why so?
                        OwedUser = owedUser == null ? null : new UserDto
                        {
                            Id = owedUser.Id,
                            Email = owedUser.Email,
                            Name = owedUser.Name
                        }
                    };
                }).ToList())

        };
            return expenseResponseDto;
        }

        public async Task<List<ExpenseResponseDto>> GetAllExpensesByGroupIdAsync(string groupId)
        {
            //var expenses = await _expenseRepository.GetAllExpensesByGroupIdAsync(groupId);
            //if(expenses == null)
            //{
            //    return null;
            //}
            //return _mapper.Map<List<ExpenseResponseDto>>(expenses);

            var expenses = await _expenseRepository.GetAllExpensesByGroupIdAsync(groupId);
            //if (expenses == null)
            //{
            //    return null;
            //}
            if (expenses == null || !expenses.Any())
            {
                return new List<ExpenseResponseDto>(); 
            }

            var expenseResponseDtos = new List<ExpenseResponseDto>();

            foreach (var expense in expenses)
            {
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
                    GroupId = expense.GroupId,
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
                            IsSettled = split.IsSettled,
                            OwedUser = owedUser == null ? null : new UserDto
                            {
                                Id = owedUser.Id,
                                Email = owedUser.Email,
                                Name = owedUser.Name
                            }
                        };
                    }).ToList())
                };

                expenseResponseDtos.Add(expenseResponseDto);
            }
            return expenseResponseDtos;
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


        
        public async Task SettleExpenseAsync(SettleExpenseRequestDto settleExpenseDto)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(settleExpenseDto.ExpenseId);
            if (expense == null)
            {
                throw new Exception("Expense not found.");
            }
            // Debug: Log the user IDs in ExpenseSplits
            var splitUserIds = expense.ExpenseSplits.Select(es => es.UserId).ToList();
            Console.WriteLine($"ExpenseSplits UserIds: {string.Join(", ", splitUserIds)}");

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

            // Update the user balance who settled the expense
            var userBalance = await _expenseRepository.GetUserBalanceAsync(settleExpenseDto.SettledByUserId, expense.GroupId);
            if (userBalance == null)
            {
                userBalance = new UserBalance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = settleExpenseDto.SettledByUserId,
                    GroupId = expense.GroupId,
                    AmountOwed = 0,
                    AmountPaid = 0,
                    IsSettled = false
                };
                _expenseRepository.Attach(userBalance);
                await _expenseRepository.CreateUserBalanceAsync(userBalance);
            }

            userBalance.AmountOwed -= split.AmountOwed;
            userBalance.AmountPaid += split.AmountOwed; 

            if (userBalance.AmountOwed == 0)
            {
                userBalance.IsSettled = true;
            }
            // Attach the user balance entity before updating
            _expenseRepository.Attach(userBalance);
            await _expenseRepository.UpdateUserBalanceAsync(userBalance);

            // Update the balance for the user who paid
            var payerBalance = await _expenseRepository.GetUserBalanceAsync(split.PaidToUserId, expense.GroupId);
            if (payerBalance == null)
            {
                payerBalance = new UserBalance
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = split.PaidToUserId,
                    GroupId = expense.GroupId,
                    AmountOwed = 0,
                    AmountPaid = 0,
                    IsSettled = false
                };
                _expenseRepository.Attach(payerBalance);
                await _expenseRepository.CreateUserBalanceAsync(payerBalance);
            }

            else
            {
                payerBalance.AmountPaid += split.AmountOwed;
                _expenseRepository.Attach(payerBalance);
                await _expenseRepository.UpdateUserBalanceAsync(payerBalance);
            }


            // Check if all splits are settled
            Console.WriteLine($"Expense ID: {expense.Id}, IsSettled Before: {expense.IsSettled}");
            Console.WriteLine($"Expense Splits Settled: {expense.ExpenseSplits.Count(es => es.IsSettled)} / {expense.ExpenseSplits.Count}");
            if (expense.ExpenseSplits.All(es => es.IsSettled))
            {
                expense.IsSettled = true;
                Console.WriteLine($"Expense ID: {expense.Id} is now marked as settled.");
                // Attach the expense entity before updating
                _expenseRepository.Attach(expense);
                await _expenseRepository.UpdateExpenseAsync(expense);
            }
            await _expenseRepository.SaveChangesAsync();
        }


        public async Task<Dictionary<string, decimal>> GetGroupBalancesAsync(string groupId)
        {
            var expenses = await _expenseRepository.GetAllExpensesByGroupIdAsync(groupId);
            var balances = new Dictionary<string, decimal>();

            foreach (var expense in expenses)
            {
                if (!balances.ContainsKey(expense.PaidByUserId))
                {
                    balances[expense.PaidByUserId] = 0;
                }
                balances[expense.PaidByUserId] += expense.Amount; // Add full amount to payer's balance

                foreach (var split in expense.ExpenseSplits)
                {
                    if (!balances.ContainsKey(split.UserId))
                    {
                        balances[split.UserId] = 0;
                    }
                    balances[split.UserId] -= split.AmountOwed; // Subtract owed amount from each user's balance
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
