using Microsoft.AspNetCore.Identity;
using ReceptsPage.Models;
using ReceptsPage.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ModelIdentity
{
    public class AppUser:IdentityUser<int>
    {
        public DateTime? Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PhotoUser { get; set; }
        public string Gender { get; set; }
        public List<ArticleP> Articles { get; set; }
        public List<BarArticleP> BarArticles { get; set; }
        public List<MainComment> MainMomments { get; set; }
        public List<ChildComment> ChildComments { get; set; }

        public AppUser()
        {
            MainMomments = new List<MainComment>();
            ChildComments = new List<ChildComment>();
        }


    }
}
