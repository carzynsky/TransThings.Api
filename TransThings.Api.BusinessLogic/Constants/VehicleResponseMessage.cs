using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class VehicleResponseMessage
    {
        public const string VehicleDataNotProvided = "Nie podano danych pojazdu.";
        public const string IncorrectYearOfProduction = "Podano nieprawidłowy rok produkcji.";
        public const string VehicleCreated = "Utworzono nowy pojazd.";
        public const string VehicleWithGivenIdNotExists = "Pojazd o podanym id nie istnieje.";
        public const string VehicleRemoved = "Usunięto pojazd.";
        public const string VehicleUpdated = "Zaktualizowano pojazd.";
    }
}
