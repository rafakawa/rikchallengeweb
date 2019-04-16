using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using rikchallengeweb.Data.Checkout;
using rikchallengeweb.Data.Card;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Text;

namespace rikchallengeweb.Pages
{
    public class CardModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CardModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            CardResponse = null;
        }

        public void OnGet()
        {
            CheckoutOrder = TempData["CheckoutOrder"] as String;
        }

        public CheckoutOrder _checkoutOrder { get; set; }
        [HiddenInput]
        [BindProperty]
        public String CheckoutOrder
        {
            get
            {
                return JsonConvert.SerializeObject(_checkoutOrder);
            }
            set
            {
                if (value != null)
                    _checkoutOrder = JsonConvert.DeserializeObject<CheckoutOrder>(value);
            }
        }

        [BindProperty]
        public String HolderName { get; set; }
        [BindProperty]
        public String Number { get; set; }
        [BindProperty]
        public String ExpirationDate { get; set; }
        [BindProperty]
        public String CVV { get; set; }


        public CardResponse CardResponse { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var card = new CreditCard();
            card.holderName = HolderName;
            card.number = Number;
            card.expirationDate = ExpirationDate;
            card.cvv = CVV;
            _checkoutOrder.payment.card = card;

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8181/card/validate");
            request.Content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var cardResponseString = response.Content.ReadAsStringAsync().Result;

                var cardResponse = JsonConvert.DeserializeObject<CardResponse>(cardResponseString);

                if (cardResponse.isValid)
                {
                    TempData["CheckoutOrder"] = CheckoutOrder;

                    return RedirectToPage("./Confirm");
                }
                else
                {
                    TempData["CheckoutOrder"] = CheckoutOrder;
                    CardResponse = JsonConvert.DeserializeObject<CardResponse>(cardResponseString);
                    return Page();
                }

            }
            TempData["CheckoutOrder"] = CheckoutOrder;

            return Page();
        }
    }
}