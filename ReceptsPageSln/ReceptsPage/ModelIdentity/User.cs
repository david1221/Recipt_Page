using Microsoft.AspNetCore.Identity;
using ReceptsPage.Models;
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
        public List<ArticleP> Articles { get; set; }
        public List<BarArticleP> BarArticles { get; set; }
        

    }
}
