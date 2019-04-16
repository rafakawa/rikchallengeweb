using System;

namespace rikchallengeweb.Data.Checkout
{

    public class CreditCard
    {
        public String holderName { get; set; }

        public String number { get; set; }

        public String expirationDate { get; set; }

        public String cvv { get; set; }

        public CreditCard()
        {

        }
    }
}