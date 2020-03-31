using System.ComponentModel.DataAnnotations;

namespace ReceptsPage.IdentityViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Էլ․ հասցե")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }

        [Display(Name = "Հիշել?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

}
