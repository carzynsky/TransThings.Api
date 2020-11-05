using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Constants
{
    public class TransporterResponseMessage
    {
        public const string TransporterDataNotProvided = "Nie podano danych przewoźnika.";
        public const string TransporterCreated = "Utworzono nowego przewoźnika.";
        public const string TransporterWithGivenIdNotExists = "Przewoźnik o podanym id nie istnieje.";
        public const string TransporterRemoved = "Usunięto przewoźnika.";
        public const string TransporterUpdated = "Zaktualizowano przewoźnika.";
    }
}
