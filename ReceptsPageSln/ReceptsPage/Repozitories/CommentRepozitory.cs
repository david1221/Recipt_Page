using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Repozitories
{
    public class CommentRepozitory : IGetComments

    {
        private readonly ArticlePContetxt _articlePContetxt;
        private readonly UserManager<AppUser> _appUsers;

        public CommentRepozitory(ArticlePContetxt articlePContetxt, UserManager<AppUser> appUsers)
        {
            _articlePContetxt = articlePContetxt;
            _appUsers = appUsers;
        }

        public IQueryable<ChildComment> GetChildCommentByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MainComment> GetMainCommentByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ChildComment> GetAllChildComments(int mainComentId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MainComment> GetAllMainComments(int ariclePId)
        {
            //  var comments = _articlePContetxt.MainComments.Include(cc=>cc.childComments).Where(a => a.articleP.ArticleId == ariclePId);
            throw new NotImplementedException();
        }

        public int SaveMainComments(MainComment comment)
        {
            if (comment.Id == default)

                _articlePContetxt.Entry(comment).State = EntityState.Added;
            else _articlePContetxt.Entry(comment).State = EntityState.Modified;
            return _articlePContetxt.SaveChanges();
                        
        }

        public int SaveChildComments(ChildComment comment)
        {
            if (comment.Id == default)

                _articlePContetxt.Entry(comment).State = EntityState.Added;
            else _articlePContetxt.Entry(comment).State = EntityState.Modified;
            return _articlePContetxt.SaveChanges();
        }

        public MainComment GetCommentByMainId(int id)
        {
            // return _articlePContetxt.MainComments.Include(x => x.appUser).Include(x => x.childComments).FirstOrDefault(x => x.Id == id);
            throw new NotImplementedException();
        }
    }
}
