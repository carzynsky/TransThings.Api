using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.DataAccess.Dto
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string Mail { get; set; }
        public int TransporterId { get; set; }
    }
}
