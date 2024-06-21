using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;
using WebAPIExample2.Services;

namespace WebAPIExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            Console.WriteLine($"LoginModel: {JsonConvert.SerializeObject(loginModel)}"); 
            var token = await _authService.Login(loginModel);
            if (!string.IsNullOrWhiteSpace(token))
            {
                return Ok(new { token });
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(User userModel)
        {
            if (await _authService.Register(userModel))
            {
                return Ok(userModel);
            }
            return NoContent();
        }
    }
}
