//using PagedList;
//using PagedList.Core;
using ReceptsPage.ModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList.Core;

namespace ReceptsPage.IdentityViewModels
{
    public class UserRoleAllViewModel
    {

        public string UserEmail { get; set; }
        public IPagedList<AppUser> AllUsersPage { get; set; }
        public List<AppUser> AllUsers { get; set; }
        public int AllUsersRoleUser { get; set; }

        public List<UserRolesOnlyViewModel> userRoles { get; set; }



        public UserRoleAllViewModel()
        {
            userRoles = new List<UserRolesOnlyViewModel>();
            AllUsers = new List<AppUser>();
        }

    }
}
