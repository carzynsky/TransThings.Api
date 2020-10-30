using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class AuthenticateResponse
    {
        public string Message { get; private set; }
        public string Role { get; private set; }

        public string Login { get; private set; }
        public string Token { get; private set; }

        public AuthenticateResponse(string message, string role, string login, string token)
        {
            Message = message;
            Role = role;
            Login = login;
            Token = token;
        }
    }
}
