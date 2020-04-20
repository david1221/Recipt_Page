using ReceptsPage.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Interfaces
{
  public  interface IGetComments
    {
        IQueryable<MainComment> GetAllMainComments(int ariclePId);
        IQueryable<ChildComment> GetAllChildComments(int mainComentId);
        IQueryable<MainComment> GetMainCommentByUserId(int userId);
        IQueryable<ChildComment> GetChildCommentByUserId(int userId);

        int SaveMainComments(MainComment comment);
        int SaveChildComments(ChildComment comment);
        MainComment GetCommentByMainId(int id);
    }
}
