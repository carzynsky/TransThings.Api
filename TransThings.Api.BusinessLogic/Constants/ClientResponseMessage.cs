using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class ClientResponseMessage
    {
        public const string ClientDataNotProvided = "Nie podano danych kontrahenta";
        public const string FirstNameOrLastNameNotProvided = "Nie podano imienia lub nazwiska kontrahenta.";
        public const string IncorrectGender = "Podano niepoprawną płeć.";
        public const string ClientWithGivenPeselAlreadyExists = "Kontrahent o podanym numerze pesel juz istnieje.";
        public const string ClientCreated = "Utworzono nowego kontrahenta.";
        public const string ClientWithGivenIdNotExists = "Kontrahent o podanym id nie istnieje.";
        public const string ClientRemoved = "Kontrahent został usunięty.";
        public const string ClientUpdated = "Kontrahent został zaktualizowany.";
    }
}
