using ExpenseSharingWebApp.DAL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.DAL.Repositories.Interface
{
    public interface IGroupRepository
    {
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> GetGroupByIdAsync(string groupId);
        Task<List<Group>> GetAllGroupsAsync();
        Task DeleteGroupAsync(string groupId);

        Task DeleteUserGroupsByGroupIdAsync(string groupId);
        Task AddUserToGroupAsync(string groupId, string userId);
        Task<bool> GroupExistsAsync(string groupId);
        Task<User> GetUserByIdAsync(string userId);
        Task AssignAdminsAsync(string groupId, List<string> adminIds);
        Task SaveChangesAsync();
    }
}
