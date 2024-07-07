using AutoMapper;
using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.DTO;
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
        private IGroupService @object;

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
                return Ok(group);
                //var groupDto = _mapper.Map<GroupResponseDto>(group); // Map entity to DTO
                //return Ok(groupDto);
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

        [HttpPost("assignAdmins")]
        public async Task<IActionResult> AssignAdmins([FromBody] AssignAdminsDto assignAdminsDto)
        {
            if (assignAdminsDto == null || string.IsNullOrEmpty(assignAdminsDto.GroupId) || assignAdminsDto.AdminIds == null || !assignAdminsDto.AdminIds.Any())
            {
                return BadRequest("Invalid input data");
            }

            try
            {
                await _groupService.AssignAdminsAsync(assignAdminsDto.GroupId, assignAdminsDto.AdminIds);
                return Ok(new { message = "Admins assigned successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while assigning admins: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
