using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.IdentityViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Էլ․ հասցե")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Նշել գաղտնաբառ")]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,40})",
         ErrorMessage = "Առնվազն 1 մեծատառ և 1 փոքրատառ տառ և թիվ")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Նշել գաղտնաբառ"), StringLength(40, MinimumLength = 6, ErrorMessage = "Ոչ պակաս քան 6 և ոչ ավել քան 40 նիշ")]
        [Compare("Password", ErrorMessage = "գաղտնաբառերը չեն համընկնում")]
        [DataType(DataType.Password)]
        [Display(Name = "Հաստատել գաղտնաբառը")]
        public string PasswordConfirm { get; set; }


        public string Code { get; set; }
    }
}
