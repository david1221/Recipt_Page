using System;
using System.ComponentModel.DataAnnotations;

namespace ReceptsPage.IdentityViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Էլ․ հասցե")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "Ընտրել սեռը")]
        [Display(Name = "Ընտրել սեռը")]
       
        public string Gender { get; set; }
        [Required(ErrorMessage = "Նշել գաղտնաբառ")]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,40})",
         ErrorMessage = "Առնվազն 1 մեծատառ և 1 փոքրատառ տառ և թիվ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Նշել գաղտնաբառ"),StringLength(40,MinimumLength=6, ErrorMessage = "Ոչ պակաս քան 6 և ոչ ավել քան 40 նիշ")]
        [Compare("Password", ErrorMessage = "գաղտնաբառերը չեն համընկնում")]

        [DataType(DataType.Password)]
        [Display(Name = "Հաստատել գաղտնաբառը")]
        public string PasswordConfirm { get; set; }
    }
}
