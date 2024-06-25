using ExpenseSharingWebApp.BLL.Services.Interface;
using ExpenseSharingWebApp.DAL.Models.Domain;
using ExpenseSharingWebApp.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSharingWebApp.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers() {
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
