using ExpenseSharingWebApp.DAL.Data;
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
            //var expenseToBeDeleted = await GetExpenseByIdAsync(expenseId);
            //if(expenseToBeDeleted != null)
            //{
            //    _expenseSharingDbContext.Expenses.Remove(expenseToBeDeleted);
            //    await _expenseSharingDbContext.SaveChangesAsync();

            //}
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
            //_expenseSharingDbContext.Entry(userBalance).State = EntityState.Modified;
            //await _expenseSharingDbContext.SaveChangesAsync();
            await _expenseSharingDbContext.SaveChangesAsync(); 
        }

        //public async Task UpdateUserBalanceAsync(UserBalance userBalance)
        //{
        //    try
        //    {
        //        _expenseSharingDbContext.Entry(userBalance).State = EntityState.Modified;
        //        await _expenseSharingDbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        Handle concurrency exception: retry or log the issue
        //        Example of retry logic:
        //        var entry = ex.Entries.Single();
        //        await entry.ReloadAsync(); // Reload the entity from the database
        //        _expenseSharingDbContext.Entry(userBalance).State = EntityState.Modified;
        //        await _expenseSharingDbContext.SaveChangesAsync();
        //    }
        //}


        public async Task UpdateExpenseAsync(Expense expense)
        {
            _expenseSharingDbContext.Expenses.Update(expense);
            await _expenseSharingDbContext.SaveChangesAsync();

        }

        public async Task<List<Expense>> GetUserExpensesAsync(string groupId, string userId)
        {
            return await _expenseSharingDbContext.Expenses
                .Include(e => e.PaidByUser)
                .Include(e => e.SplitAmong)
                .Where(e => e.GroupId == groupId && e.SplitAmong.Any(u => u.Id == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpenseSplit>> GetExpenseSplitsAsync(string userId, string groupId)
        {
            return await _expenseSharingDbContext.ExpenseSplits
                .Include(es => es.Expense)
                .Where(es => es.UserId == userId && es.Expense.GroupId == groupId)
                .ToListAsync();
        }

    }
}
