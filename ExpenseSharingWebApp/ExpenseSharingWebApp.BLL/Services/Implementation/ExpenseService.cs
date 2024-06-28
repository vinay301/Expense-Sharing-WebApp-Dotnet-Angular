using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
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

        public ExpenseService(IExpenseRepository expenseRepository, IGroupRepository groupRepository,IMapper mapper)
        {
            this._expenseRepository = expenseRepository;
            this._groupRepository = groupRepository;
            this._mapper = mapper;
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
            decimal individualShare = expense.Amount / expense.SplitAmong.Count; //got exception over this line
            expense.ExpenseSplits = createExpenseRequestDto.SplitWithUserIds.Select(userId => new ExpenseSplit
            {
                UserId = userId,
                Amount = individualShare,
                ExpenseId = expense.Id
            }).ToList();

            // Update balances for each user
            foreach (var userId in createExpenseRequestDto.SplitWithUserIds)
            {
                if (userId == paidByUser.Id) continue;
                await UpdateUserBalanceAsync(userId, expense.GroupId, individualShare);

                //var userBalance = await _expenseRepository.GetUserBalanceAsync(userId, expense.GroupId) ?? new UserBalance { Id = Guid.NewGuid().ToString(), UserId = userId, GroupId = expense.GroupId, AmountOwed = 0 };
                //userBalance.AmountOwed += individualShare;
                //await _expenseRepository.UpdateUserBalanceAsync(userBalance); //exception here
            }

            // Update balance for the payer
            await UpdateUserBalanceAsync(createExpenseRequestDto.PaidByUserId, expense.GroupId, -(expense.Amount - individualShare));

            var createdExpense = await _expenseRepository.CreateExpenseAsync(expense);
            return _mapper.Map<ExpenseResponseDto>(createdExpense);
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
            return _mapper.Map<ExpenseResponseDto>(expense);
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
            if(expenseToBeUpdate == null) 
            {
                throw new Exception("Expense Not Found!");
            }

            _mapper.Map(updateExpenseRequestDto, expenseToBeUpdate);

            // Re-calculate the expense splits
            expenseToBeUpdate.ExpenseSplits.Clear();
            foreach (var userId in updateExpenseRequestDto.SplitWithUserIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                    expenseToBeUpdate.ExpenseSplits.Add(new ExpenseSplit { UserId = user.Id, Amount = updateExpenseRequestDto.Amount / updateExpenseRequestDto.SplitWithUserIds.Count });
                }
                else
                {
                    throw new Exception($"User with ID {userId} not found.");
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

            if (expense.IsSettled)
            {
                throw new Exception("Expense is already settled.");
            }

            expense.IsSettled = true;

            foreach (var split in expense.ExpenseSplits)
            {
                var userBalance = await _expenseRepository.GetUserBalanceAsync(split.UserId, expense.GroupId);
                userBalance.AmountOwed -= split.Amount;

                if (userBalance.AmountOwed == 0)
                {
                    userBalance.IsSettled = true;
                }

                await _expenseRepository.UpdateUserBalanceAsync(userBalance);
            }

            await _expenseRepository.UpdateExpenseAsync(expense);
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
                    if (split.UserId == expense.PaidByUserId)
                    {
                        balances[split.UserId] += split.Amount;
                    }
                    else
                    {
                        balances[split.UserId] -= split.Amount;
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
