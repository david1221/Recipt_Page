using ReceptsPage.ModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models.Comments
{
    public class ChildComment 

    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int appUserId { get; set; }
        public int mainCommentId { get; set; }

    }

}
