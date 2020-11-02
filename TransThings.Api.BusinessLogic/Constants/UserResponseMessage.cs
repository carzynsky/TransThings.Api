using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class UserResponseMessage
    {
        public const string UserDataNotProvided = "Nie podano danych użytkownika.";
        public const string FirstNameOrLastNameOrLoginNotProvided = "Nie podano imienia, nazwiska lub loginuużytkownika.";
        public const string UserWithGivenLoginAlreadyExists = "Użytkownik o podanym loginie już istnieje.";
        public const string IncorrectGender = "Podano niepoprawną płeć.";
        public const string NewUserCreated = "Utworzono nowego użytkownika.";
        public const string NewPasswordNotProvided = "Nie podano nowego hasła.";
        public const string NewPasswordsNotIdentical = "Podane hasła nie są takie same.";
        public const string UserWithGivenIdNotExists = "Użytkownik o podanym id nie istnieje.";
        public const string IncorrectOldPassword = "Niepoprawne stare hasło.";
        public const string NewPasswordHasToBeDifferent = "Nowe hasło musi być inne od starego.";
        public const string NotSafePassword = "Podane hasło nie jest bezpieczne.";
        public const string PasswordChanged = "Hasło zostało zmienione.";
        public const string UserRemoved = "Użytkownik został usunięty.";
        public const string UserUpdated = "Użytkownik został zaktualizowany.";
    }
}
