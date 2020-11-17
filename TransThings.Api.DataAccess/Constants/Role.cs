using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.DataAccess.Constants
{
    public class Role
    {
        public const string Admin = "Admin";
        public const string Forwarder = "Forwarder";
        public const string OrderEmployee = "Orderer";

        public const int AdminId = 1;
        public const int ForwarderId = 2;
        public const int OrdererId = 3;
    }
}
