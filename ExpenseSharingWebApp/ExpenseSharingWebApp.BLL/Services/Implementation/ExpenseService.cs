﻿using AutoMapper;
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

        //public async Task<ExpenseResponseDto> CreateExpenseAsync(CreateExpenseRequestDto createExpenseRequestDto)
        //{
        //    var expense = _mapper.Map<Expense>(createExpenseRequestDto);
        //    expense.Id = Guid.NewGuid().ToString();
        //    expense.Date = DateTime.UtcNow;
        //    expense.IsSettled = false;

        //    var group = await _groupRepository.GetGroupByIdAsync(createExpenseRequestDto.GroupId);
        //    if (group == null)
        //    {
        //        throw new Exception("Group not found.");
        //    }

        //    var paidByUser = await _groupRepository.GetUserByIdAsync(createExpenseRequestDto.PaidByUserId);
        //    if (paidByUser == null)
        //    {
        //        throw new Exception("User who paid not found.");
        //    }
        //    expense.SplitAmong = new List<User>();
        //    foreach (var userId in createExpenseRequestDto.SplitWithUserIds)
        //    {
        //        var user = await _groupRepository.GetUserByIdAsync(userId);
        //        if (user != null)
        //        {
        //            expense.SplitAmong.Add(user);
        //        }
        //        else
        //        {
        //            throw new Exception($"User with ID {userId} not found.");
        //        }
        //    }
        //    decimal individualShare;
        //    if(expense.SplitAmong.Count == 1)
        //    {
        //        individualShare = expense.Amount / 2;
        //    }
        //    else
        //    {
        //        individualShare = expense.Amount / expense.SplitAmong.Count; //got exception over this line
        //    }

        //    expense.ExpenseSplits = createExpenseRequestDto.SplitWithUserIds.Select(userId => new ExpenseSplit
        //    {
        //        UserId = userId,
        //        PaidToUserId = createExpenseRequestDto.PaidByUserId,
        //        ExpenseId = expense.Id,
        //        AmountOwed = userId == createExpenseRequestDto.PaidByUserId ? 0 : individualShare,
        //        AmountPaid = userId == createExpenseRequestDto.PaidByUserId ? expense.Amount : 0,
        //        IsSettled = false

        //    }).ToList();
        //    // Add an ExpenseSplit for the PaidByUserId if not already added
        //    if (!expense.ExpenseSplits.Any(es => es.UserId == createExpenseRequestDto.PaidByUserId))
        //    {
        //        expense.ExpenseSplits.Add(new ExpenseSplit
        //        {
        //            UserId = createExpenseRequestDto.PaidByUserId,
        //            PaidToUserId = createExpenseRequestDto.PaidByUserId,
        //            ExpenseId = expense.Id,
        //            AmountOwed = 0,
        //            AmountPaid = expense.Amount,
        //            IsSettled = false
        //        });
        //    }


        //    await _expenseRepository.CreateExpenseAsync(expense);
        //    return _mapper.Map<ExpenseResponseDto>(expense);

        //}

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
                individualShare = expense.Amount / createExpenseRequestDto.SplitWithUserIds.Count;
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
                IsSettled = expense.IsSettled, //this value is false
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
            }

            userBalance.AmountOwed -= split.AmountOwed;
            userBalance.AmountPaid += split.AmountOwed; 

            if (userBalance.AmountOwed == 0)
            {
                userBalance.IsSettled = true;
            }

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
            }

            payerBalance.AmountPaid += split.AmountOwed; 
            await _expenseRepository.UpdateUserBalanceAsync(payerBalance);

            //// Check if all splits are settled except for the one who paid
            //if (expense.ExpenseSplits.Where(es => es.UserId != split.PaidToUserId).All(es => es.IsSettled))
            //{
            //    expense.IsSettled = true;
            //    await _expenseRepository.UpdateExpenseAsync(expense);
            //}
            //else
            //{
            //    await _expenseRepository.UpdateExpenseAsync(expense);
            //}

            // Check if all splits are settled
            Console.WriteLine($"Expense ID: {expense.Id}, IsSettled Before: {expense.IsSettled}");
            Console.WriteLine($"Expense Splits Settled: {expense.ExpenseSplits.Count(es => es.IsSettled)} / {expense.ExpenseSplits.Count}");
            if (expense.ExpenseSplits.All(es => es.IsSettled))
            {
                expense.IsSettled = true;
                Console.WriteLine($"Expense ID: {expense.Id} is now marked as settled.");
                await _expenseRepository.UpdateExpenseAsync(expense);
            }
         
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
