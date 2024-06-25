using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.BLL.Services.Interface
{
    public interface IGroupService
    {
        Task<GroupResponseDto> CreateGroupAsync(CreateGroupRequestDto groupDto);
        Task<GroupResponseDto> GetGroupByIdAsync(string groupId);
        Task<List<GroupResponseDto>> GetAllGroupsAsync();
        Task AddUsersToGroupAsync(string groupId, List<string> userIds);
        Task DeleteGroupAsync(string groupId);
        Task DeleteUserFromGroupAsync(string groupId, string userId);
    }
}
