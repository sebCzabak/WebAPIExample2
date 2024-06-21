using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

namespace WebAPIExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound($"Lack of user with id: {userId}");

        }
        [HttpGet]
        [Route("users")]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            if (users != null && users.Any())
            {
                return Ok(users);
            }
            return NotFound($"Lack of users");
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User userModel)
        {
            if (await _userService.AddUser(userModel))
            {
                return Ok(userModel);
            }
            return NoContent();

        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(User userModel)
        {
            var existingUser = await _userService.GetUser(userModel.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateUser(userModel);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var existingUser = await _userService.GetUser(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}
