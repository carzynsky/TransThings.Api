using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class DriverResponseMessage
    {
        public const string DriverDataNotProvided = "Nie podano danych kierowcy.";
        public const string FirstNameOrLastNameNotProvided = "Nie podano imienia lub nazwiska kierowcy.";
        public const string PeselNumberNotProvided = "Nie podano numeru pesel kierowcy.";
        public const string IncorrectGender = "Podano niepoprawną płeć.";
        public const string DriverWithGivenPeselAlreadyExists = "Kierowca o podanym numerze pesel już istnieje.";
        public const string DriverCreated = "Utworzono nowego kierowcę.";
        public const string DriverWithGivenIdNotExists = "Kierowca o podanym id nie istnieje.";
        public const string DriverRemoved = "Usunięto kierowcę.";
        public const string DriverBirthdateNotProvided = "Nie podano daty urodzenia kierowcy.";
        public const string DriverUpdated = "Zaktualizowano kierowcę.";
    }
}
