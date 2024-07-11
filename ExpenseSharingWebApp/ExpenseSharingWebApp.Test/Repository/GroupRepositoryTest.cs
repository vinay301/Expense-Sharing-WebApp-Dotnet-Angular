using Duende.IdentityServer.EntityFramework.Options;
using ExpenseSharingWebApp.DAL.Data;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseSharingWebApp.Test.Repository
{
    public class GroupRepositoryTest : IDisposable
    {
        private readonly ExpenseSharingDbContext _context;
        private readonly GroupRepository _repository;

        public GroupRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ExpenseSharingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var operationalStoreOptions = Options.Create(new OperationalStoreOptions());
            _context = new ExpenseSharingDbContext(options, operationalStoreOptions);
            _repository = new GroupRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public async Task CreateGroupAsync_ShouldAddGroupToDatabase()
        {
            // Arrange
            var group = new Group { Id = "1", Name = "Test Group" };

            // Act
            var result = await _repository.CreateGroupAsync(group);

            // Assert
            Assert.Equal(group, result);
            Assert.Equal(1, await _context.Groups.CountAsync());
            var savedGroup = await _context.Groups.FirstOrDefaultAsync();
            Assert.Equal("Test Group", savedGroup.Name);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnGroup()
        {
            // Arrange
            var group = new Group { Id = "1", Name = "Test Group" };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetGroupByIdAsync("1");

            // Assert
            Assert.Equal(group.Id, result.Id);
            Assert.Equal(group.Name, result.Name);
        }

        [Fact]
        public async Task GetAllGroupsAsync_ShouldReturnAllGroups()
        {
            // Arrange
            var groups = new List<Group>
            {
                new Group { Id = "1", Name = "Group 1" },
                new Group { Id = "2", Name = "Group 2" }
            };
            _context.Groups.AddRange(groups);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllGroupsAsync();
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Name == "Group 1");
            Assert.Contains(result, g => g.Name == "Group 2");
        }

        [Fact]
        public async Task DeleteGroupAsync_ShouldRemoveGroupFromDatabase()
        {
            // Arrange
            var group = new Group { Id = "1", Name = "Test Group" };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteGroupAsync("1");

            // Assert
            Assert.Equal(0, await _context.Groups.CountAsync());
        }

        [Fact]
        public async Task AddUserToGroupAsync_ShouldAddUserGroupToDatabase()
        {
            // Arrange
            var group = new Group { Id = "1", Name = "Test Group" };
            var user = new User { Id = "1", UserName = "TestUser" };
            _context.Groups.Add(group);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            await _repository.AddUserToGroupAsync("1", "1");
            // Assert
            var userGroup = await _context.Set<UserGroup>().FirstOrDefaultAsync();
            Assert.NotNull(userGroup);
            Assert.Equal("1", userGroup.GroupId);
            Assert.Equal("1", userGroup.UserId);
        }

        [Fact]
        public async Task GroupExistsAsync_ShouldReturnTrueForExistingGroup()
        {
            // Arrange
            var group = new Group { Id = "1", Name = "Test Group" };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GroupExistsAsync("1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GroupExistsAsync_ShouldReturnFalseForNonExistingGroup()
        {
            // Act
            var result = await _repository.GroupExistsAsync("999");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserGroupsByGroupIdAsync_ShouldRemoveUserGroupsForGroup()
        {
            // Arrange
            var group = new Group { Id = "1", Name = "Test Group" };
            var users = new List<User>
            {
                new User { Id = "1", UserName = "User1" },
                new User { Id = "2", UserName = "User2" }
            };
            _context.Groups.Add(group);
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
            var userGroups = new List<UserGroup>
            {
                new UserGroup { UserId = "1", GroupId = "1" },
                new UserGroup { UserId = "2", GroupId = "1" }
            };
            _context.Set<UserGroup>().AddRange(userGroups);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteUserGroupsByGroupIdAsync("1");
            // Assert
            var remainingUserGroups = await _context.Set<UserGroup>().Where(ug => ug.GroupId == "1").ToListAsync();
            Assert.Empty(remainingUserGroups);
        }
        [Fact]
        public async Task AssignAdminsAsync_ValidRequest_UpdatesAdminsAndSavesChanges()
        {
            // Arrange
            var groupId = "group1";
            var adminIds = new List<string> { "admin1", "admin2" };

            var group = new Group
            {
                Id = groupId,
                Name = "Test Group",
                Admins = new List<UserGroupAdmin>()
            };

            var admins = new List<User>
            {
                new User { Id = "admin1", UserName = "Admin1" },
                new User { Id = "admin2", UserName = "Admin2" }
            };

            _context.Groups.Add(group);
            _context.Users.AddRange(admins);
            await _context.SaveChangesAsync();

            // Act
            await _repository.AssignAdminsAsync(groupId, adminIds);

            // Assert
            var updatedGroup = await _context.Groups
                .Include(g => g.Admins)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            Assert.NotNull(updatedGroup);
            Assert.Equal(2, updatedGroup.Admins.Count);
            Assert.Contains(updatedGroup.Admins, a => a.UserId == "admin1");
            Assert.Contains(updatedGroup.Admins, a => a.UserId == "admin2");
        }




    }
}
