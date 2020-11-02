using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class AuthResponseMessage
    {
        public const string UserWithThisLoginNotExists = "Użytkownik o podanym loginie już istnieje.";
        public const string WrongPassword = "Niepoprawne hasło.";
        public const string LoggedSucessfuly = "Zalogowano.";
        public const string LoginAndPasswordNotProvided = "Login i hasło nie zostało podane.";
        public const string LoginNotProvided = "Nie podano loginu.";
        public const string PasswordNotProvided = "Nie podano hasła";
        public const string ErrorWhileGeneratingToken = "Wystąpił błąd podczas generowania tokenu.";
    }
}
