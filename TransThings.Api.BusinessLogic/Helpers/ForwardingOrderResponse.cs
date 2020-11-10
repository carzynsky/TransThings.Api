using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class ForwardingOrderResponse
    {
        public bool IsSuccessful{ get; set; }
        public string Message { get; set; }
        public int? ForwardingOrderId { get; set; }
        public ForwardingOrderResponse(bool isSuccessful, string message, int? forwardingOrderId)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            ForwardingOrderId = forwardingOrderId;
        }
    }
}
