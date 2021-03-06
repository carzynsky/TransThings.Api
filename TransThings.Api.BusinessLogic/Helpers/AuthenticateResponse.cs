﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class AuthenticateResponse
    {
        public string Message { get; private set; }
        public string Role { get; private set; }
        public int? UserId { get; set; }
        public string Login { get; private set; }
        public string Initials { get; private set; }
        public string Token { get; private set; }

        public AuthenticateResponse(string message, int? userId, string role, string login, string initials, string token)
        {
            Message = message;
            UserId = userId;
            Role = role;
            Login = login;
            Initials = initials;
            Token = token;
        }
    }
}
