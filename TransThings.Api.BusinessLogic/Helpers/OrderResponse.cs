using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class OrderResponse
    {
        public bool IsSuccessful { get; private set; }
        public string Message { get; private set; }
        public int? OrderId { get; set; }
        public OrderResponse(bool isSuccessful, string message, int? orderId)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            OrderId = orderId;
        }
    }
}
