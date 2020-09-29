using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.DataAccess.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string CompanyFullName { get; set; }
        public string CompanyShortName { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string StreetName { get; set; }
        public string NIP { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ContactPhoneNumber1 { get; set; }
        public string ContactPhoneNumber2 { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
}
