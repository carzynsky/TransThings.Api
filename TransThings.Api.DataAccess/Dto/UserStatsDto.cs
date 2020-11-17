using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.DataAccess.Dto
{
    public class UserStatsDto
    {
        public int AdminsQuantity { get; set; }
        public int ForwardersQuantity { get; set; }
        public int OrderersQuantity { get; set; }
        public UserDto? LastLoggedUser { get; set; }
        public int TodaysLoginAttempts { get; set; }
        public double TodaysSuccessfulLoginRate { get; set; }
    }
}
