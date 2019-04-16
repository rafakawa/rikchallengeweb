using System;

namespace rikchallengeweb.Data.Checkout
{
    public class CheckoutOrder
    {
        public Client client { get; set; }

        public Buyer buyer { get; set; }

        public Payment payment { get; set; }

        public CheckoutOrder()
        {
            buyer = new Buyer();
            payment = new Payment();
        }

        public CheckoutOrder(String clientId) : this()
        {
            client = new Client();
            client.id = clientId;
        }

    }
}