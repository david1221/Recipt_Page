using Microsoft.EntityFrameworkCore;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.Models.VideoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Repozitories
{
    public class BarArticlesRepozitory : IBarArticles
    {
        private readonly ArticlePContetxt _articlePContetxt;
        public BarArticlesRepozitory(ArticlePContetxt articlePContetxt)
        {
            _articlePContetxt = articlePContetxt;
        }
        public IQueryable<BarArticleP> GetBarArticles()
        {
            return _articlePContetxt.BarArticles.OrderByDescending(x => x.DateAdded.Value).Include(x => x.BarCategory).Include(x => x.AppUser);

        }
        public IQueryable<BarArticleP> GetBarArticlesWithoutBarCategory()
        {
            return _articlePContetxt.BarArticles.OrderByDescending(x => x.DateAdded.Value);

        }
        public IQueryable<VideoModelA> GetBarVideoArticles()
        {
            return _articlePContetxt.videoModels.OrderByDescending(x => x.date);

        }
        public BarArticleP BarGetArticlePById(int id)
        {
            return _articlePContetxt.BarArticles.Include(c=>c.BarCategory).Include(a=>a.AppUser).FirstOrDefault(c => c.BarArticleId == id);
        }
        public IQueryable<AppUser> GetBarArticlesByUser()
        {
            //return appUsers.OrderByDescending(x => x.DateAdded.Value).Include(x => x.SubCategory).Include(x => x.AppUser);
            return _articlePContetxt.AppUsers.Include(a => a.BarArticles).ThenInclude(s => s.BarCategory);

        }
        public IQueryable<AppUser> GetBarArticlesByUserWithoutBarCategory()
        {

            return _articlePContetxt.AppUsers.Include(a => a.BarArticles);
        }
        public IQueryable<BarCategory> BarCategories()
        {
            return _articlePContetxt.BarCategories;
        }

        public IQueryable<BarArticleP> BarCategoryById(int id)
        {
        
            return _articlePContetxt.BarArticles.Where(c => c.BarCategoryId == id).Include(c => c.BarCategory).OrderByDescending(x=>x.DateAdded);
        }
        public string BarCategoryByIdSingle(int id)
        {
            try
            {
                return _articlePContetxt.BarCategories.Where(x => x.BarCategoryId == id).FirstOrDefault().Name;
            }
            catch (Exception)
            {

                return "Նման Բաժին գոյություն չունի";
            }
        }
        
        public void DeleteBarArticle(BarArticleP BarArticle)
        {
            _articlePContetxt.BarArticles.Remove(BarArticle);
            _articlePContetxt.SaveChanges();
        }
        public void DeleteVideoBarArticle(VideoModelA model)
        {
            _articlePContetxt.videoModels.Remove(model);
            _articlePContetxt.SaveChanges();
        }

        public int SaveBarArticle(BarArticleP barArticleP)
        {
            if (barArticleP.BarArticleId == default)
            
                _articlePContetxt.Entry(barArticleP).State = EntityState.Added;
            else
            _articlePContetxt.Entry(barArticleP).State = EntityState.Modified;
            

            _articlePContetxt.SaveChanges();

            return barArticleP.BarArticleId;
        }

        public int SaveBarVideoArticle(VideoModelA videoModelA)
        {
            if (videoModelA.Id == default)

                _articlePContetxt.Entry(videoModelA).State = EntityState.Added;
            else
                _articlePContetxt.Entry(videoModelA).State = EntityState.Modified;


            _articlePContetxt.SaveChanges();

            return videoModelA.Id;
        }

    }
}
