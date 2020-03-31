using ReceptsPage.ModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.IdentityViewModels
{
    public class UserRoleAllViewModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public List<AppUser> AllUsers { get; set; }
        public IList<string> UserRoles { get; set; }

        public List<AppRole> AllRoles { get; set; }


        public UserRoleAllViewModel()
        {
            AllUsers = new List<AppUser>();
            UserRoles = new List<string>();
            AllRoles = new List<AppRole>();

            
        }

    }
}
