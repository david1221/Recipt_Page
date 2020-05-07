using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.IdentityViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Նշել Ձեր էլ․ հասցեն")]
        [EmailAddress]
        [Display(Name = "Էլեկտրոնային հասցե")]
        public string Email { get; set; }
    }
}
