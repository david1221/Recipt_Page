using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReceptsPage.IdentityViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Գրել Էլ․հասցեն")]
        [Display(Name = "Էլ․ հասցե")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Գրել գաղտնաբառը")]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }

        [Display(Name = "Հիշել?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public List<AuthenticationScheme> ExternalLogins { get; set; }
        public LoginViewModel()
        {
            ExternalLogins = new List<AuthenticationScheme>();
        }
    }

}
