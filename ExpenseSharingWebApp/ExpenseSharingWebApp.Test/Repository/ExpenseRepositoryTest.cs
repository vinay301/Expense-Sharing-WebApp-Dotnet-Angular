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
    public class ExpenseRepositoryTest : IDisposable
    {
        private readonly ExpenseSharingDbContext _context;
        private readonly ExpenseRepository _repository;

        public ExpenseRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ExpenseSharingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
            _context = new ExpenseSharingDbContext(options, operationalStoreOptions);
            _repository = new ExpenseRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task CreateExpenseAsync_ShouldAddExpenseToDatabase()
        {
            // Arrange
            var expense = new Expense { Id = "1", Description = "Test Expense", Amount = 100 };

            // Act
            var result = await _repository.CreateExpenseAsync(expense);

            // Assert
            Assert.Equal(expense, result);
            Assert.Equal(1, await _context.Expenses.CountAsync());
            var savedExpense = await _context.Expenses.FirstOrDefaultAsync();
            Assert.Equal("Test Expense", savedExpense.Description);
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ShouldReturnExpense()
        {
            // Arrange
            var expense = new Expense { Id = "1", Description = "Test Expense", Amount = 100 };
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetExpenseByIdAsync("1");

            // Assert
            Assert.Equal(expense.Id, result.Id);
            Assert.Equal(expense.Description, result.Description);
        }

        [Fact]
        public async Task GetAllExpensesByGroupIdAsync_ShouldReturnAllExpensesForGroup()
        {
            // Arrange
            var groupId = "group1";
            var expenses = new List<Expense>
            {
                new Expense { Id = "1", Description = "Expense 1", GroupId = groupId },
                new Expense { Id = "2", Description = "Expense 2", GroupId = groupId },
                new Expense { Id = "3", Description = "Expense 3", GroupId = "otherGroup" }
            };
            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();
            // Act
            var result = await _repository.GetAllExpensesByGroupIdAsync(groupId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Description == "Expense 1");
            Assert.Contains(result, e => e.Description == "Expense 2");
        }

        [Fact]
        public async Task DeleteExpenseAsync_ShouldRemoveExpenseFromDatabase()
        {
            // Arrange
            var expense = new Expense { Id = "1", Description = "Test Expense" };
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteExpenseAsync("1");

            // Assert
            Assert.Equal(0, await _context.Expenses.CountAsync());
        }

        [Fact]
        public async Task CreateUserBalanceAsync_ShouldAddUserBalanceToDatabase()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "TestUser" };
            var group = new Group { Id = "1", Name = "TestGroup" };
            _context.Users.Add(user);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var userBalance = new UserBalance
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "1",
                GroupId = "1",
                AmountOwed = 100,
                AmountPaid = 50,
                IsSettled = false
            };

            // Act
            await _repository.CreateUserBalanceAsync(userBalance);

            // Assert
            Assert.Equal(1, await _context.UserBalances.CountAsync());
            var savedUserBalance = await _context.UserBalances.FirstOrDefaultAsync();
            Assert.Equal(100, savedUserBalance.AmountOwed);
            Assert.Equal(50, savedUserBalance.AmountPaid);
            Assert.False(savedUserBalance.IsSettled);
        }

        [Fact]
        public async Task GetUserBalanceAsync_ShouldReturnUserBalance()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "TestUser" };
            var group = new Group { Id = "1", Name = "TestGroup" };
            _context.Users.Add(user);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var userBalance = new UserBalance
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "1",
                GroupId = "1",
                AmountOwed = 100,
                AmountPaid = 50,
                IsSettled = false
            };

            _context.UserBalances.Add(userBalance);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetUserBalanceAsync("1", "1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.AmountOwed);
            Assert.Equal(50, result.AmountPaid);
            Assert.False(result.IsSettled);
        }

        [Fact]
        public async Task UpdateUserBalanceAsync_ShouldUpdateExistingUserBalance()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "TestUser" };
            var group = new Group { Id = "1", Name = "TestGroup" };
            _context.Users.Add(user);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var userBalanceId = Guid.NewGuid().ToString();
            var userBalance = new UserBalance
            {
                Id = userBalanceId,
                UserId = "1",
                GroupId = "1",
                AmountOwed = 100,
                AmountPaid = 50,
                IsSettled = false
            };
            _context.UserBalances.Add(userBalance);
            await _context.SaveChangesAsync();

            userBalance.AmountOwed = 150;
            userBalance.AmountPaid = 100;
            userBalance.IsSettled = true;

            // Act
            await _repository.UpdateUserBalanceAsync(userBalance);

            // Assert
            var updatedUserBalance = await _context.UserBalances.FindAsync(userBalanceId);
            Assert.Equal(150, updatedUserBalance.AmountOwed);
            Assert.Equal(100, updatedUserBalance.AmountPaid);
            Assert.True(updatedUserBalance.IsSettled);
        }

        //[Fact]
        //public async Task GetUserExpensesAsync_ShouldReturnUserExpenses()
        //{
        //    var user1 = new User { Id = "user1", UserName = "TestUser" };
        //    var user2 = new User { Id = "otherUser", UserName = "OtherUser" };
        //    var group = new Group { Id = "group1", Name = "TestGroup" };

        //    _context.Users.Add(user1);
        //    _context.Users.Add(user2);
        //    _context.Groups.Add(group);
        //    _context.SaveChanges();

        //    var expenses = new List<Expense>
        //{
        //    new Expense
        //    {
        //        Id = "1",
        //        Description = "Expense 1",
        //        GroupId = "group1",
        //        PaidByUserId = "user1",
        //    },
        //    new Expense
        //    {
        //        Id = "2",
        //        Description = "Expense 2",
        //        GroupId = "group1",
        //        PaidByUserId = "user1",
        //    },
        //    new Expense
        //    {
        //        Id = "3",
        //        Description = "Expense 3",
        //        GroupId = "group1",
        //        PaidByUserId = "user1",
        //    }
        //};

        //    _context.Expenses.AddRange(expenses);
        //    _context.SaveChanges();

        //        var expenseSplits = new List<ExpenseSplit>
        //        {
        //            new ExpenseSplit { UserId = "user1", ExpenseId = "1" },
        //            new ExpenseSplit { UserId = "user1", ExpenseId = "2" },
        //            new ExpenseSplit { UserId = "otherUser", ExpenseId = "3" }
        //        };

        //    _context.ExpenseSplits.AddRange(expenseSplits);
        //    _context.SaveChanges();

        //    // Act
        //    var result = await _repository.GetUserExpensesAsync("group1", "user1");

        //    // Assert
        //    Assert.Equal(2, result.Count);
        //    Assert.Contains(result, e => e.Description == "Expense 1");
        //    Assert.Contains(result, e => e.Description == "Expense 2");

        //}
        [Fact]
        public async Task GetUserExpensesAsync_ShouldReturnUserExpenses()
        {
            // Arrange
            var user1 = new User { Id = "user1", UserName = "TestUser" };
            var user2 = new User { Id = "otherUser", UserName = "OtherUser" };
            var group = new Group { Id = "group1", Name = "TestGroup" };

            _context.Users.AddRange(user1, user2);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var expenses = new List<Expense>
    {
        new Expense
        {
            Id = "1",
            Description = "Expense 1",
            GroupId = "group1",
            PaidByUser = user1,
            SplitAmong = new List<User> { user1, user2 }
        },
        new Expense
        {
            Id = "2",
            Description = "Expense 2",
            GroupId = "group1",
            PaidByUser = user2,
            SplitAmong = new List<User> { user1 }
        },
        new Expense
        {
            Id = "3",
            Description = "Expense 3",
            GroupId = "group1",
            PaidByUser = user2,
            SplitAmong = new List<User> { user2 }
        }
    };

            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetUserExpensesAsync("group1", "user1");

            // Debug information
            Console.WriteLine($"Number of expenses returned: {result.Count}");
            foreach (var expense in result)
            {
                Console.WriteLine($"Expense ID: {expense.Id}, Description: {expense.Description}");
                Console.WriteLine($"PaidByUser: {expense.PaidByUser?.Id ?? "null"}");
                Console.WriteLine($"SplitAmong count: {expense.SplitAmong?.Count ?? 0}");
                if (expense.SplitAmong != null)
                {
                    foreach (var user in expense.SplitAmong)
                    {
                        Console.WriteLine($"  User in SplitAmong: {user.Id}");
                    }
                }
                Console.WriteLine("---");
            }

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Description == "Expense 1");
            Assert.Contains(result, e => e.Description == "Expense 2");
            Assert.DoesNotContain(result, e => e.Description == "Expense 3");

            // Additional assertions to verify PaidByUser and SplitAmong are loaded
            foreach (var expense in result)
            {
                Assert.NotNull(expense.PaidByUser);
                Assert.NotEmpty(expense.SplitAmong);
                Assert.Contains(expense.SplitAmong, u => u.Id == "user1");
            }

            // Verify specific relationships
            var expense1 = result.First(e => e.Id == "1");
            Assert.Equal("user1", expense1.PaidByUser.Id);
            Assert.Contains(expense1.SplitAmong, u => u.Id == "user2");

            var expense2 = result.First(e => e.Id == "2");
            Assert.Equal("otherUser", expense2.PaidByUser.Id);
            Assert.Single(expense2.SplitAmong);
            Assert.Equal("user1", expense2.SplitAmong.First().Id);
        }


    //    [Fact]
    //    public async Task GetUserExpensesAsync_ShouldReturnUserExpenses()
    //    {
    //        // Arrange
    //        var user1 = new User { Id = "user1", UserName = "TestUser" };
    //        var user2 = new User { Id = "otherUser", UserName = "OtherUser" };
    //        var group = new Group { Id = "group1", Name = "TestGroup" };

    //        _context.Users.AddRange(user1, user2);
    //        _context.Groups.Add(group);
    //        await _context.SaveChangesAsync();

    //        var expenses = new List<Expense>
    //{
    //    new Expense
    //    {
    //        Id = "1",
    //        Description = "Expense 1",
    //        GroupId = "group1",
    //        PaidByUser = user1,
    //        SplitAmong = new List<User> { user1, user2 }
    //    },
    //    new Expense
    //    {
    //        Id = "2",
    //        Description = "Expense 2",
    //        GroupId = "group1",
    //        PaidByUser = user2,
    //        SplitAmong = new List<User> { user1 }
    //    },
    //    new Expense
    //    {
    //        Id = "3",
    //        Description = "Expense 3",
    //        GroupId = "group1",
    //        PaidByUser = user2,
    //        SplitAmong = new List<User> { user2 }
    //    }
    //};

    //        _context.Expenses.AddRange(expenses);
    //        await _context.SaveChangesAsync();

    //        // Act
    //        var result = await _repository.GetUserExpensesAsync("group1", "user1");

    //        // Debug information
    //        Console.WriteLine($"Number of expenses returned: {result.Count}");
    //        foreach (var expense in result)
    //        {
    //            Console.WriteLine($"Expense ID: {expense.Id}, Description: {expense.Description}");
    //            Console.WriteLine($"PaidByUser: {expense.PaidByUser?.Id ?? "null"}");
    //            Console.WriteLine($"SplitAmong count: {expense.SplitAmong?.Count ?? 0}");
    //            if (expense.SplitAmong != null)
    //            {
    //                foreach (var user in expense.SplitAmong)
    //                {
    //                    Console.WriteLine($"  User in SplitAmong: {user.Id}");
    //                }
    //            }
    //            Console.WriteLine("---");
    //        }

    //            // Assert
    //            Assert.Equal(2, result.Count);
    //        Assert.Contains(result, e => e.Description == "Expense 1");
    //        Assert.Contains(result, e => e.Description == "Expense 2");
    //        Assert.DoesNotContain(result, e => e.Description == "Expense 3");

    //        // Additional assertions to verify PaidByUser and SplitAmong are loaded
    //        foreach (var expense in result)
    //        {
    //            Assert.NotNull(expense.PaidByUser);
    //            Assert.NotEmpty(expense.SplitAmong);
    //            Assert.Contains(expense.SplitAmong, u => u.Id == "user1");
    //        }

    //        // Verify specific relationships
    //        var expense1 = result.First(e => e.Id == "1");
    //        Assert.Equal("user1", expense1.PaidByUser.Id);
    //        Assert.Contains(expense1.SplitAmong, u => u.Id == "user2");

    //        var expense2 = result.First(e => e.Id == "2");
    //        Assert.Equal("otherUser", expense2.PaidByUser.Id);
    //        Assert.Single(expense2.SplitAmong);
    //        Assert.Equal("user1", expense2.SplitAmong.First().Id);
    //    }

        [Fact]
        public async Task GetExpenseSplitsAsync_ShouldReturnExpenseSplits()
        {
            // Arrange
            var userId = "user1";
            var groupId = "group1";

            // Create and add User and Group
            var user = new User { Id = userId, UserName = "TestUser" };
            var group = new Group { Id = groupId, Name = "TestGroup" };
            _context.Users.Add(user);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var expenses = new List<Expense>
            {
                new Expense { Id = "expense1", GroupId = groupId, Description = "Expense 1" },
                new Expense { Id = "expense2", GroupId = groupId, Description = "Expense 2" },
                new Expense { Id = "expense3", GroupId = groupId, Description = "Expense 3" }
            };
            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { UserId = userId, ExpenseId = "expense1" },
                new ExpenseSplit { UserId = userId, ExpenseId = "expense2" },
                new ExpenseSplit { UserId = "otherUser", ExpenseId = "expense3" }
            };
            _context.ExpenseSplits.AddRange(expenseSplits);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetExpenseSplitsAsync(userId, groupId);

            // Assert
            Assert.Equal(2, result.Count());
           
            Assert.Contains(result, es => es.ExpenseId == "expense1");
            Assert.Contains(result, es => es.ExpenseId == "expense2");
            Assert.DoesNotContain(result, es => es.ExpenseId == "expense3");
        }
    }
}
