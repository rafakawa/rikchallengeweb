using System;

namespace rikchallengeweb.Data.Checkout
{

    public class Payment
    {
        public decimal amount { get; set; }

        public String type { get; set; }

        public CreditCard card { get; set; }

        public Payment()
        {
            card = new CreditCard();
        }
    }
}