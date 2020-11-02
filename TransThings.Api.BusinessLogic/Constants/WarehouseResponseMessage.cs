using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class WarehouseResponseMessage
    {
        public const string WarehouseDataNotProvided = "Nie podano danych magazynu.";
        public const string LocationDataNotProvided = "Nie podano danych lokalizacyjnych.";
        public const string WarehouseCreated = "Utworzono nowy magazyn.";
        public const string WarehouseWithGivenIdNotExists = "Magazyn o podanym id nie istnieje.";
        public const string WarehouseRemoved = "Usunięto magazyn.";
        public const string WarehouseUpdated = "Zaktualizowano magazyn.";
    }
}
