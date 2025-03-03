using ChatMeeting.Core.Domain.DTOs;
using ChatMeeting.Core.Domain.Interfaces.Repositories;
using ChatMeeting.Core.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMeeting.Core.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByLogin(registerUserDto.Username);
                if (existingUser != null)
                {
                    _logger.LogWarning($"User with login {registerUserDto.Username} already exists");
                    throw new InvalidOperationException("User with this login already exists");
                }

                var user = new User(registerUserDto.Username, registerUserDto.Password);
                await _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error ocurred while registering user: {registerUserDto.Username}");
                throw new InvalidProgramException();
            }
        }
    }
}
