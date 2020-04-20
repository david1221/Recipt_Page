using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.IdentityViewModels
{
    public class UserRolesOnlyViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public IList<string> UserRoles { get; set; }

    }
}
