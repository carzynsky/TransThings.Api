using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class GenericResponse
    {
        public bool IsSuccessful { get; private set; }
        public string Message { get; private set; }
        public GenericResponse(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
