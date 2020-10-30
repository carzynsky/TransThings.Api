using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.DataAccess.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string PeselNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string Login { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public int UserRoleId { get; set; }
        public string UserRole { get; set; }
    }
}
