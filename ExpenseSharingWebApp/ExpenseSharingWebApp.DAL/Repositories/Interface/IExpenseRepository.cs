﻿using ExpenseSharingWebApp.DAL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Repositories.Interface
{
    public interface IExpenseRepository
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<Expense> GetExpenseByIdAsync(string expenseId);
        Task<List<Expense>> GetAllExpensesByGroupIdAsync(string groupId);
        Task CreateUserBalanceAsync(UserBalance userBalance);
        Task<UserBalance> GetUserBalanceAsync(string userId, string groupId);
        Task UpdateUserBalanceAsync(UserBalance userBalance);
        Task DeleteExpenseAsync(string expenseId);
        Task UpdateExpenseAsync(Expense expense);
    }
}
