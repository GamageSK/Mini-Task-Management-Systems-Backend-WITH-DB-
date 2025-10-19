using Microsoft.AspNetCore.Mvc;
using TaskManagement_Backend.Models;
using TaskManagement_Backend.Services;

namespace TaskManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.MobileNumber) ||
                string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Mobile number, user name, and password are required.");
            }

            var result = await _userService.RegisterUserAsync(user);

            if (!result)
                return BadRequest("Mobile number already exists.");

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.MobileNumber) ||
                string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest("Mobile number and password are required.");
            }

            var result = await _userService.LoginAsync(loginRequest);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }
    }
}