using Duende.IdentityServer.EntityFramework.Options;
using ExpenseSharingWebApp.DAL.Data;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseSharingWebApp.Test.Repository
{
    public class UserRepositoryTest
    {
        private readonly ExpenseSharingDbContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ExpenseSharingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
            _context = new ExpenseSharingDbContext(options, operationalStoreOptions);
            _repository = new UserRepository(_context);

            // Seed the database
            _context.Users.AddRange(
                new User { Id = "user1", Name = "User 1", Email = "user1@example.com" },
                new User { Id = "user2", Name = "User 2", Email = "user2@example.com" }
            );
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Act
            var result = await _repository.GetAllUsersAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetUserByIdAsync_ExistingUser_ReturnsUser()
        {
            // Act
            var result = await _repository.GetUserByIdAsync("user1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User 1", result.Name);
            Assert.Equal("user1@example.com", result.Email);
        }

    }
}
