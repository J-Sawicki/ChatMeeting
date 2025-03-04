using ChatMeeting.Core.Domain.DTOs;
using ChatMeeting.Core.Domain.Interfaces.Repositories;
using ChatMeeting.Core.Domain.Interfaces.Services;
using ChatMeeting.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace ChatMeeting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService userService, ILogger<AuthController> logger)
        {
            _authService = userService;
            _logger = logger;
        }

        [HttpPut("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            try
            {
                await _authService.RegisterUser(registerUser);
                return Ok(new { message = "User registered succesfully" });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Registration attempt failed: User already exists with login: {registerUser.Username}");
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"An unexpected erorr occured during registration for user: {registerUser.Username}");
                return StatusCode(500, "An unexpected error occured during registration");
            }
        }
    }
}
