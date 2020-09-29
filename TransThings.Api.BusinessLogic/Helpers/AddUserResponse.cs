using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class AddUserResponse
    {
        public string Message { get; private set; }
        public string GeneratedPassword { get; private set; }
        public bool IsSuccessful { get; private set; }

        public AddUserResponse(bool isSuccessful, string message, string generatedPassword)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            GeneratedPassword = generatedPassword;
        }
    }
}
