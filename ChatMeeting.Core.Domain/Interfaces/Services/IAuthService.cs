﻿using ChatMeeting.Core.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMeeting.Core.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
    }
}
