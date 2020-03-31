using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ModelIdentity
{
    public class AppRole:IdentityRole<int>
    {
        public AppRole() : base() { }

        public AppRole(string name)
            : base(name)
        { }

    }
}
