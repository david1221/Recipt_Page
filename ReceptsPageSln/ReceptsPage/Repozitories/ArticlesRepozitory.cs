using Microsoft.EntityFrameworkCore;
using ReceptsPage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    public class ArticlesRepozitory: IGetArticles
    {
        private readonly ArticlePContetxt _articlePContetxt;
        public ArticlesRepozitory(ArticlePContetxt articlePContetxt)
        {
            _articlePContetxt = articlePContetxt;
        }
        public IQueryable<ArticleP> GetArticles()
        {
            return _articlePContetxt.Articles.OrderByDescending(x => x.DateAdded.Value);

        }
        //public IQueryable<Category> GetArticlesBySession(int a)
        //{
        //    return articlePContetxt.Categories.ToList.Property(x=>x.Name).Where(x => x.CategoryId == a).Select(x => x.Name).ToArray();

        //}
        public IQueryable<ArticleP> SubCategoryById(int id)
        {
            return _articlePContetxt.Articles.Where(x=>x.SubCategoryId==id).OrderByDescending(x => x.DateAdded);
        }


        public string SubCategoryByIdSingle(int id)
        {
            return _articlePContetxt.SubCategories.Single(x => x.SubCategoryId == id).Name;
        }


        public IQueryable<SubCategory> SubCategories()
        {
            return _articlePContetxt.SubCategories;
        }

        public ArticleP GetArticlePById(int id)
        {
            return _articlePContetxt.Articles.Single(x => x.ArticleId == id);
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
            _articlePContetxt.Articles.Remove(article);
            _articlePContetxt.SaveChanges();
        }
    }
}
