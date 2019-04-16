using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using rikchallengeweb.Data.Checkout;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Text;

namespace rikchallengeweb.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public ConfirmModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
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


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8181/order/process");
            request.Content = new StringContent(CheckoutOrder, Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            TempData["Result"] = response.Content.ReadAsStringAsync().Result;

            return RedirectToPage("./Result");
        }
    }
}