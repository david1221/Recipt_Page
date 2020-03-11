using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ModelIdentity
{
    public class AppUser:IdentityUser<int>
    {
        public int Year { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
