using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using rikchallengeweb.Data.Checkout;
using Newtonsoft.Json;
using System;

namespace rikchallengeweb.Pages
{
    public class IndentityModel : PageModel
    {
        public IndentityModel()
        {

        }

        public void OnGet()
        {
            _checkoutOrder = new CheckoutOrder("Client1");
        }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Cpf { get; set; }

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

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Buyer = new Buyer();
            Buyer.name = Name;
            Buyer.eMail = Email;
            Buyer.cpf = Cpf;
            _checkoutOrder.buyer = Buyer;

            TempData["CheckoutOrder"] = CheckoutOrder;

            return RedirectToPage("./Payment");
        }
    }
}