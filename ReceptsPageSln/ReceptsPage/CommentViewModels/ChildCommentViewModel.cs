using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.CommentViewModels
{
    public class ChildCommentViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Ավելացնելու համար լրացրեք դաշտը")]
        public string Text { get; set; }
        public int MainCommentID { get; set; }
    }
}
