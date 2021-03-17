using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    public class ArticlesRepozitory : IGetArticles
    {
        private readonly ArticlePContetxt _articlePContetxt;
        private readonly UserManager<AppUser> appUsers;
        private readonly CacheResponse _cache;
        //   private readonly IConfiguration configuration;

        public ArticlesRepozitory(ArticlePContetxt articlePContetxt, UserManager<AppUser> appUsers, CacheResponse cache/*, IConfiguration configuration*/)
        {
            _articlePContetxt = articlePContetxt;
            this.appUsers = appUsers;
            //  configuration = configuration;
        }
        public async Task<IList<ArticleP>> GetArticles()
        {
            try
            {
                var a = await _articlePContetxt.Articles
               .OrderByDescending(x => x.DateAdded.Value)
               .Include(x => x.SubCategory)
               .Where(x => x.AdminConfirm == true)
               .ToListAsync();
                return a;
            }
            catch (Exception)
            {

                return null;
            }


        }
        public async Task<IList<ArticleP>> GetArticlesByAdmin()
        {
            try
            {
                var a = await _articlePContetxt.Articles
               .OrderByDescending(x => x.DateAdded.Value)
               .Include(x => x.SubCategory)
               .Include(x => x.AppUser)
               .ToListAsync();
                return a;
            }
            catch (Exception)
            {

                return null;
            }


        }
      
        public async Task<IList<ArticleP>> GetArticlesWithoutSubCategory()
        {
            return await _articlePContetxt.Articles.OrderByDescending(x => x.DateAdded.Value).ToListAsync();

        }
        public IQueryable<AppUser> GetArticlesByUser()
        {
            //return appUsers.OrderByDescending(x => x.DateAdded.Value).Include(x => x.SubCategory).Include(x => x.AppUser);
            //return _articlePContetxt.AppUsers.Include(mc => mc.MainMomments).Include(cc => cc.ChildComments).Include(a => a.Articles).ThenInclude(s => s.SubCategory);
            return _articlePContetxt.AppUsers.Include(a => a.Articles).ThenInclude(s => s.SubCategory);
        }
        public IList<ArticleP> GetArticlesFromCacheRep()
        {
            int a = 100;

            var articles = _cache.GetArticlesFromCache(a);
            return (IList<ArticleP>)articles;

        }
        public IQueryable<AppUser> GetArticlesByUserWithoutSubCategory()
        {

            return _articlePContetxt.AppUsers.Include(a => a.Articles);
        }
        //public IQueryable<Category> GetArticlesBySession(int a)
        //{
        //    return articlePContetxt.Categories.ToList.Property(x=>x.Name).Where(x => x.CategoryId == a).Select(x => x.Name).ToArray();

        //}
        public IQueryable<ArticleP> SubCategoryById(int id)
        {

            return _articlePContetxt.Articles.Where(x => x.SubCategoryId == id).OrderByDescending(x => x.DateAdded).Include(i => i.SubCategory);
        }


        public string SubCategoryByIdSingle(int id)
        {
            try
            {
                return _articlePContetxt.SubCategories.Where(x => x.SubCategoryId == id).FirstOrDefault().Name;
            }
            catch (Exception)
            {

                return "Նման Բաժին գոյություն չունի";
            }
        }

        public IQueryable<SubCategory> SubCategories()
        {
            return _articlePContetxt.SubCategories;
        }

        public ArticleP GetArticlePById(int id)
        {
            try
            {
                return _articlePContetxt.Articles.Include(x => x.SubCategory).Include(x => x.AppUser).FirstOrDefault(x => x.ArticleId == id);
            }
            catch (Exception)
            {


                return new ArticleP { ArticleId = 0 };
            }



        }

        public int SaveArticle(ArticleP articleP)
        {
            if (articleP.ArticleId == default)

                _articlePContetxt.Entry(articleP).State = EntityState.Added;

            else _articlePContetxt.Entry(articleP).State = EntityState.Modified;
            _articlePContetxt.SaveChanges();


            return articleP.ArticleId;
        }

        public void DeleteArticle(ArticleP article)
        {

            try
            {
                _articlePContetxt.Articles.Remove(article);
                _articlePContetxt.SaveChanges();
            }
            catch (Exception)
            {
            }

        }
        //public IEnumerable<ArticleP> ArticlePsImage()
        //{

        //    var model = _articlePContetxt.Articles;
        //    return model;
        //}
    }
}
