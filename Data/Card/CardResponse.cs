using System;

namespace rikchallengeweb.Data.Card
{
    public class CardResponse
    {
        public bool isValid { get; set; }
        public String brand { get; set; }

        public CardResponse()
        {
            isValid = true;
            brand = "";
        }
    }
}