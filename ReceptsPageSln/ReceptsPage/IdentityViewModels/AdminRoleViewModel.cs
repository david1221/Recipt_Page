//using PagedList.Core;
using ReceptsPage.ModelIdentity;
using System.Collections.Generic;


namespace ReceptsPage.IdentityViewModels
{
    public class AdminRoleViewModel
    {
        public List<AppUser> AdminUsers { get; set; }
        public List<UserRolesOnlyViewModel> userRoles { get; set; }
        public AdminRoleViewModel()
        {
            AdminUsers = new List<AppUser>();
            userRoles = new List<UserRolesOnlyViewModel>();
        }
    }
}
