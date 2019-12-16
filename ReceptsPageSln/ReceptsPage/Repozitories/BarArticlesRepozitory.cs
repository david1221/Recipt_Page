using Microsoft.EntityFrameworkCore;
using ReceptsPage.Interfaces;
using ReceptsPage.Models;
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
        public BarArticleP BarGetArticlePById(int id)
        {
            return _articlePContetxt.BarArticles.Include(c=>c.BarCategory).FirstOrDefault(c => c.BarArticleId == id);
        }

        public IQueryable<BarCategory> BarCategories()
        {
            return _articlePContetxt.BarCategories;
        }

        public IQueryable<BarArticleP> BarCategoryById(int id)
        {
        
            return _articlePContetxt.BarArticles.Where(c => c.BarCategoryId == id).Where(m=>m.ImgGeneral!=null).Include(c => c.BarCategory).OrderByDescending(x=>x.DateAdded);
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

        public int SaveBarArticle(BarArticleP barArticleP)
        {
            if (barArticleP.BarArticleId == default)
            
                _articlePContetxt.Entry(barArticleP).State = EntityState.Added;
            else
            _articlePContetxt.Entry(barArticleP).State = EntityState.Modified;
            

            _articlePContetxt.SaveChanges();

            return barArticleP.BarArticleId;
        }

        
    }
}
