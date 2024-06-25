using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.DTO.Request;
using ExpenseSharingWebApp.DAL.Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSharingWebApp.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            this._groupService = groupService;
            this._mapper = mapper;
        }

        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequestDto createGroupRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var group = await _groupService.CreateGroupAsync(createGroupRequestDto);
                var groupDto = _mapper.Map<GroupResponseDto>(group); // Map entity to DTO
                return Ok(groupDto);
                //var group = await _groupService.CreateGroupAsync(groupDto);
                //return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet]
        [Route("GetGroupById/{groupId}")]
        public async Task<IActionResult> GetGroupByIdAsync(string groupId)
        {
            var group = await _groupService.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                return NotFound();
            }

                return Ok(group);
        }
        [HttpPost("AddUsersInGroup/{groupId}")]
        public async Task<IActionResult> AddUsersToGroup(string groupId, [FromBody] List<string> userIds)
        {
            await _groupService.AddUsersToGroupAsync(groupId, userIds);
            return Ok();
        }
        [HttpDelete("DeleteGroup/{groupId}")]
        public async Task<IActionResult> DeleteGroup(string groupId)
        {
            await _groupService.DeleteGroupAsync(groupId);
            return Ok();
        }

        [HttpDelete("DeleteUsersInGroup/{groupId}/{userId}")]
        public async Task<IActionResult> DeleteUserInGroup(string groupId, string userId)
        {
            await _groupService.DeleteUserFromGroupAsync(groupId, userId);
            return Ok();
        }
    }
}
