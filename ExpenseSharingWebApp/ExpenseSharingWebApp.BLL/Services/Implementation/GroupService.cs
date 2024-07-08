using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using ExpenseSharingWebApp.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpenseSharingWebApp.BLL.Services.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            this._groupRepository = groupRepository;
            this._mapper = mapper;
        }

        public async Task<GroupResponseDto> CreateGroupAsync(CreateGroupRequestDto groupDto)
        {
            var group = _mapper.Map<DAL.Models.Domain.Group>(groupDto);
            group.Id = Guid.NewGuid().ToString();
            group.CreatedDate = DateTime.UtcNow;

            // Check for unique group exists
            if (await _groupRepository.GroupExistsAsync(group.Id))
            {
                throw new Exception("GroupId must be unique");
            }

            // Ensure the number of members does not exceed 10
            if (groupDto.MemberIds.Count > 10)
            {
                throw new Exception("Group cannot have more than 10 members");
            }

            // Add members to the group
            group.UserGroups = new List<UserGroup>();
            foreach (var memberId in groupDto.MemberIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(memberId); 
                if (user != null)
                {
                    group.UserGroups.Add(new UserGroup { UserId = user.Id, Group = group });
                }
                else
                {
                    throw new Exception($"User with ID {memberId} not found.");
                }
            }
            // Add admins to the group
            group.Admins = new List<UserGroupAdmin>();
            foreach (var adminId in groupDto.AdminIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(adminId);
                if (user != null)
                {
                    group.Admins.Add(new UserGroupAdmin { UserId = user.Id, Group = group });
                }
                else
                {
                    throw new Exception($"Admin with ID {adminId} not found.");
                }
            }

            var createdGroup = await _groupRepository.CreateGroupAsync(group);
           
            var groupResponseDto = _mapper.Map<GroupResponseDto>(createdGroup);

            // Retrieve admin details and map them to the response DTO
            groupResponseDto.Admins = new List<UserDto>();
            foreach (var admin in group.Admins)
            {
                var adminUser = await _groupRepository.GetUserByIdAsync(admin.UserId);
                if (adminUser != null)
                {
                    groupResponseDto.Admins.Add(_mapper.Map<UserDto>(adminUser));
                }
            }

            return groupResponseDto;
           // return _mapper.Map<GroupResponseDto>(createdGroup);
        }

        public async Task<GroupResponseDto> GetGroupByIdAsync(string groupId)
        {
           
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                return null;
            }
            //var groupResponseDto = _mapper.Map<GroupResponseDto>(group);

            //// Map Admins to the response DTO
            //groupResponseDto.Admins = group.Admins.Select(admin => _mapper.Map<UserDto>(admin.User)).ToList();

            //return groupResponseDto;
            return _mapper.Map<GroupResponseDto>(group);
        }

        public async Task<List<GroupResponseDto>> GetAllGroupsAsync()
        {
            
            var groups = await _groupRepository.GetAllGroupsAsync();
            if (groups == null)
            {
                return null;
            }
            //var groupResponseDtos = _mapper.Map<List<GroupResponseDto>>(groups);

            //// Map Admins to each group's response DTO
            //foreach (var group in groups)
            //{
            //    var groupResponseDto = groupResponseDtos.First(gr => gr.Id == group.Id);
            //    groupResponseDto.Admins = group.Admins.Select(admin => _mapper.Map<UserDto>(admin.User)).ToList();
            //}

            //return groupResponseDtos;
            return _mapper.Map<List<GroupResponseDto>>(groups);
        }

        public async Task AddUsersToGroupAsync(string groupId, List<string> userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                    await _groupRepository.AddUserToGroupAsync(groupId, userId);
                }
                else
                {
                    throw new Exception($"User with ID {userId} not found.");
                }
            }
        }


        public async Task DeleteGroupAsync(string groupId)
        {
            await _groupRepository.DeleteUserGroupsByGroupIdAsync(groupId);
            await _groupRepository.DeleteGroupAsync(groupId);
        }

        public async Task DeleteUserFromGroupAsync(string groupId, string userId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                throw new Exception($"Group with ID {groupId} not found.");
            }

            var userExistsInGroup = group.UserGroups?.FirstOrDefault(ug => ug.UserId == userId); 
            if(userExistsInGroup == null)
            {
                throw new Exception($"User with ID {userId} not found in group {groupId}.");
            }

            group.UserGroups.Remove(userExistsInGroup);
            await _groupRepository.SaveChangesAsync();
        }

        public async Task AssignAdminsAsync(string groupId, List<string> adminIds)
        {
            await _groupRepository.AssignAdminsAsync(groupId, adminIds);
        }
    }
}
