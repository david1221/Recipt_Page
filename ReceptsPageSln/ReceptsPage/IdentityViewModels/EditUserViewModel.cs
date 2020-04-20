using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.IdentityViewModels
{
    public class EditUserViewModel
    {
        public string EmailUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PhotoUser { get; set; }
        public DateTime? Birthdate { get; set; }

         }
}
