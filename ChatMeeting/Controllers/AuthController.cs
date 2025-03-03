using ChatMeeting.Core.Domain.Interfaces.Repositories;
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
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<JsonResult> GetUser()
        {
            var user = new User()
            {
                Username = "test",
                Password = "haslo"
            };

            await _userRepository.AddUser(user);
            return Json(user);
        }
    }
}
