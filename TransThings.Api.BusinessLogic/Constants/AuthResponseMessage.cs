using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class AuthResponseMessage
    {
        public const string UserWithThisLoginNotExists = "User with this login does not exist.";
        public const string WrongPassword = "Wrong password.";
        public const string LoggedSucessfuly = "Logged successfuly.";
        public const string LoginAndPasswordNotProvided = "Login and password has not been provided";
        public const string LoginNotProvided = "Login has not been provided";
        public const string PasswordNotProvided = "Password has not been provided";
        public const string ErrorWhileGeneratingToken = "Error appeared while generating token.";
    }
}
