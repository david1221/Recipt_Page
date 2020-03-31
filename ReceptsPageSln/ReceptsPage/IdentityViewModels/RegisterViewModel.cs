using System.ComponentModel.DataAnnotations;

namespace ReceptsPage.IdentityViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Էլ․ հասցե")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ծննդյան տարեթիվ")]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "գաղտնաբառերը չեն համընկնում")]
        [DataType(DataType.Password)]
        [Display(Name = "Հաստատել գաղտնաբառը")]
        public string PasswordConfirm { get; set; }
    }
}
