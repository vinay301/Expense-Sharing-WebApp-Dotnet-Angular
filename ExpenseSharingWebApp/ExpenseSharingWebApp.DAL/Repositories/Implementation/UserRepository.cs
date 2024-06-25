using ExpenseSharingWebApp.DAL.Data;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using ExpenseSharingWebApp.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseSharingDbContext _expenseSharingDbContext;

        public UserRepository(ExpenseSharingDbContext expenseSharingDbContext)
        {
            this._expenseSharingDbContext = expenseSharingDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _expenseSharingDbContext.Users.ToListAsync();
        }
    }
}
