using System;

namespace rikchallengeweb.Data.Checkout
{
    public class CheckoutResponse
    {
        public String processOrderId {get;set;}
        public String status { get; set; }
        public String payloadType { get; set; }
        public String payload { get; set; }

        public CheckoutResponse()
        {
        }

    }
}