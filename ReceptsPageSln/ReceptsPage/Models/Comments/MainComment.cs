using ReceptsPage.ModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models.Comments
{
    public class MainComment 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public AppUser appUser { get; set; }
        public ArticleP articleP { get; set; }
        public List<ChildComment> childComments { get; set; }
        public MainComment()
        {
            childComments = new List<ChildComment>();
           
        }
    }
}
