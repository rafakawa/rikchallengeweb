using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using rikchallengeweb.Data.Checkout;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace rikchallengeweb.Pages
{
    public class PaymentModel : PageModel
    {
        private Dictionary<String, String> paymentTypes = new Dictionary<string, string>();
        public PaymentModel()
        {
            paymentTypes.Add("BOLETO", "Boleto");
            paymentTypes.Add("CREDIT_CARD", "Cartão de Crédito");
            PaymentTypeOptions = new SelectList(paymentTypes, "Key", "Value");
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
        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Value { get; set; }

        [BindProperty]
        [Required]
        public String PaymentType { get; set; }

        public SelectList PaymentTypeOptions { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var payment = new Payment();
            payment.amount = Value;
            payment.type = PaymentType;
            _checkoutOrder.payment = payment;
            TempData["CheckoutOrder"] = CheckoutOrder;

            if (PaymentType == "BOLETO")
            {
                return RedirectToPage("./Confirm");
            }
            else if (PaymentType == "CREDIT_CARD")
            {
                return RedirectToPage("./Card");
            }

            return Page();
        }
    }
}