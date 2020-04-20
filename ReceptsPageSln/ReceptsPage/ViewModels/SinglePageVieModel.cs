using ReceptsPage.CommentViewModels;
using ReceptsPage.Models;
using ReceptsPage.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ViewModels
{
    public class SinglePageVieModel
    {
        public ArticleP articleP { get; set; }
        public MainCommentViewModel mainCommentViewModel{get;set;}
        public IQueryable<MainComment> mainComments { get; set; }
        public ChildCommentViewModel ChildCommentViewModel { get; set; }
    }
}
