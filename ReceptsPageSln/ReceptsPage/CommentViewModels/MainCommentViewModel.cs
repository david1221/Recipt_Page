using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.CommentViewModels
{
    public class MainCommentViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NameUser { get; set; }
        [Required(ErrorMessage ="Ավելացնելու համար լրացրեք դաշտը")]
        public string Text { get; set; }
       public int ArticleId { get; set; }
    }
}
