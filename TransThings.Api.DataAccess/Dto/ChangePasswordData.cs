using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.DataAccess.Dto
{
    public class ChangePasswordData
    {
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}
