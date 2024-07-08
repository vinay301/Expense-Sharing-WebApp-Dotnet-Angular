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
    public class GroupRepository : IGroupRepository
    {
        private readonly ExpenseSharingDbContext _expenseSharingDbContext;

        public GroupRepository(ExpenseSharingDbContext expenseSharingDbContext)
        {
            this._expenseSharingDbContext = expenseSharingDbContext;
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            _expenseSharingDbContext.Groups.Add(group);
            await _expenseSharingDbContext.SaveChangesAsync();
            return group;
        }

        public async Task<Group> GetGroupByIdAsync(string groupId)
        {
            return await _expenseSharingDbContext.Groups.Include(g => g.UserGroups)
                                   .ThenInclude(ug => ug.User)
                                   .Include(g => g.Expenses)
                                   .Include(g => g.Admins)
                                   .ThenInclude(ga => ga.User)
                                   .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _expenseSharingDbContext.Groups.Include(g => g.UserGroups)
                                    .ThenInclude(ug => ug.User)
                                    .Include(g => g.Expenses)
                                    .Include(g => g.Admins)
                                    .ThenInclude(ga => ga.User)
                                    .ToListAsync();
        }

        public async Task DeleteGroupAsync(string groupId)
        {
            var groupToBeDeleted = await _expenseSharingDbContext.Groups.FindAsync(groupId);
            if(groupToBeDeleted != null)
            {
                _expenseSharingDbContext.Groups.Remove(groupToBeDeleted);
                await _expenseSharingDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUserGroupsByGroupIdAsync(string groupId)
        {
            var userGroups = _expenseSharingDbContext.UserGroups.Where(ug => ug.GroupId == groupId);
            _expenseSharingDbContext.UserGroups.RemoveRange(userGroups);
            await _expenseSharingDbContext.SaveChangesAsync();
        }

        public async Task AddUserToGroupAsync(string groupId, string userId)
        {
            var groupInWhichUserIsAdded = await _expenseSharingDbContext.Groups.FindAsync(groupId);
            var userToAddInGroup = await _expenseSharingDbContext.Users.FindAsync(userId);

            if(groupInWhichUserIsAdded != null && userToAddInGroup != null)
            {
                var userGroup = new UserGroup
                {
                    UserId = userId,
                    GroupId = groupId
                };
                _expenseSharingDbContext.Set<UserGroup>().Add(userGroup);
                await _expenseSharingDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> GroupExistsAsync(string groupId)
        {
            return await _expenseSharingDbContext.Groups.AnyAsync(g => g.Id == groupId);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _expenseSharingDbContext.Users.FindAsync(userId);
        }

        public async Task AssignAdminsAsync(string groupId, List<string> adminIds)
        {
            var group = await _expenseSharingDbContext.Groups
                                .Include(g => g.Admins)
                                .FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
            {
                throw new Exception("Group not found.");
            }

            var admins = await _expenseSharingDbContext.Users
                .Where(u => adminIds.Contains(u.Id))
                .ToListAsync();

            group.Admins = admins.Select(a => new UserGroupAdmin
            {
                UserId = a.Id,
                GroupId = group.Id,
                User = a,
                Group = group
            }).ToList();

            _expenseSharingDbContext.Groups.Update(group);
            await _expenseSharingDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _expenseSharingDbContext.SaveChangesAsync();
        }
    }
}
