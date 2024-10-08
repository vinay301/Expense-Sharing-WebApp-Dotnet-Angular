﻿using ExpenseSharingWebApp.DAL.Data;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Repositories.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseSharingDbContext _expenseSharingDbContext;

        public ExpenseRepository(ExpenseSharingDbContext expenseSharingDbContext)
        {
            this._expenseSharingDbContext = expenseSharingDbContext;
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            _expenseSharingDbContext.Attach(entity);
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            await _expenseSharingDbContext.Expenses.AddAsync(expense);
            await _expenseSharingDbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> GetExpenseByIdAsync(string expenseId)
        {
            return await _expenseSharingDbContext.Expenses.Include(e => e.PaidByUser)
               .Include(e => e.ExpenseSplits)
               .ThenInclude(es => es.User)
               .FirstOrDefaultAsync(e => e.Id == expenseId);
        }

        public async Task<List<Expense>> GetAllExpensesByGroupIdAsync(string groupId)
        {
            return await _expenseSharingDbContext.Expenses.Where(e => e.GroupId == groupId)
                .Include(e => e.PaidByUser)
                .Include(e => e.ExpenseSplits)
                .ThenInclude(es => es.User)
                .ToListAsync();
        }

        public async Task DeleteExpenseAsync(string expenseId)
        {
            var expense = await _expenseSharingDbContext.Expenses
                                    .Include(e => e.ExpenseSplits)
                                    .Include(e => e.SplitAmong)
                                    .FirstOrDefaultAsync(e => e.Id == expenseId);

            if (expense != null)
            {
                _expenseSharingDbContext.ExpenseSplits.RemoveRange(expense.ExpenseSplits);
                _expenseSharingDbContext.Expenses.Remove(expense);
                await _expenseSharingDbContext.SaveChangesAsync();
            }
        }

        public async Task CreateUserBalanceAsync(UserBalance userBalance)
        {
            if (string.IsNullOrEmpty(userBalance.Id))
            {
                userBalance.Id = Guid.NewGuid().ToString();
            }
            _expenseSharingDbContext.UserBalances.Add(userBalance);
            await _expenseSharingDbContext.SaveChangesAsync();
        }
        public async Task<UserBalance> GetUserBalanceAsync(string userId, string groupId)
        {
            return await _expenseSharingDbContext.UserBalances.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.GroupId == groupId);
        }

        public async Task UpdateUserBalanceAsync(UserBalance userBalance)
        {
            if (string.IsNullOrEmpty(userBalance.Id))
            {
                userBalance.Id = Guid.NewGuid().ToString();
                _expenseSharingDbContext.UserBalances.Add(userBalance);
            }
            else
            {
                _expenseSharingDbContext.UserBalances.Update(userBalance);
            }
          
            await _expenseSharingDbContext.SaveChangesAsync(); 
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _expenseSharingDbContext.Expenses.Update(expense);
            await _expenseSharingDbContext.SaveChangesAsync();

        }

        public async Task<List<Expense>> GetUserExpensesAsync(string groupid, string userid)
        {
            return await _expenseSharingDbContext.Expenses
                .AsNoTracking()
                .Include(e => e.PaidByUser)
                .Include(e => e.ExpenseSplits)
                .ThenInclude(es => es.User)
                .Where(e => e.GroupId == groupid && e.ExpenseSplits.Any(es => es.UserId == userid))
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpenseSplit>> GetExpenseSplitsAsync(string userId, string groupId)
        {
            return await _expenseSharingDbContext.ExpenseSplits
                .Include(es => es.Expense)
                .Where(es => es.UserId == userId && es.Expense.GroupId == groupId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _expenseSharingDbContext.SaveChangesAsync();
        }

    }
}
