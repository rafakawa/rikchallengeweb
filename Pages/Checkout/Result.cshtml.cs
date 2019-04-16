using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using rikchallengeweb.Data.Checkout;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace rikchallengeweb.Pages
{
    public class ResultModel : PageModel
    {
        public ResultModel()
        {
        }

        public void OnGet()
        {
            Result = TempData["Result"] as String;
        }

        [BindProperty]
        public CheckoutResponse CheckoutResponse { get; set; }

        [BindProperty]
        public String Result
        {
            get
            {
                return JsonConvert.SerializeObject(CheckoutResponse);
            }
            set
            {
                if (value != null)
                    CheckoutResponse = JsonConvert.DeserializeObject<CheckoutResponse>(value);
            }
        }

    }
}