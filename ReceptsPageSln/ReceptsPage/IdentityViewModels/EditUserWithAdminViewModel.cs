using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.IdentityViewModels
{
    public class EditUserWithAdminViewModel
    {

        public string EmailUser { get; set; }
        public bool EmailConfirme { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirm { get; set; }
        public byte[] PhotoUser { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
