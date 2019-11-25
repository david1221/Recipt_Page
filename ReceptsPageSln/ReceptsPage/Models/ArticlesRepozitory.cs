using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Models
{
    public class ArticlesRepozitory
    {
        private readonly ArticlePContetxt articlePContetxt;
        public ArticlesRepozitory(ArticlePContetxt articlePContetxt)
        {
            this.articlePContetxt = articlePContetxt;
        }
        public IQueryable<ArticleP> GetArticles()
        {
            return articlePContetxt.Articles.OrderByDescending(x => x.DateAdded.Value);

        }
        //public IQueryable<Category> GetArticlesBySession(int a)
        //{
        //    return articlePContetxt.Categories.ToList.Property(x=>x.Name).Where(x => x.CategoryId == a).Select(x => x.Name).ToArray();

        //}
        public IQueryable<ArticleP> SubCategoryById(int id)
        {
            return articlePContetxt.Articles.Where(x=>x.SubCategoryId==id).OrderByDescending(x => x.DateAdded);
        }


        public string SubCategoryByIdSingle(int id)
        {
            return articlePContetxt.SubCategories.Single(x => x.SubCategoryId == id).Name;
        }


        public IQueryable<SubCategory> SubCategories()
        {
            return articlePContetxt.SubCategories;
        }
        public ArticleP GetArticlePById(int id)
        {
            return articlePContetxt.Articles.Single(x => x.ArticleId == id);
        }
        public int SaveArticle(ArticleP articleP)
        {
            if (articleP.ArticleId == default)

                articlePContetxt.Entry(articleP).State = EntityState.Added;

            else articlePContetxt.Entry(articleP).State = EntityState.Modified;
            articlePContetxt.SaveChanges();


            return articleP.ArticleId;
        }
        public void DeleteArticle(ArticleP article)
        {
            articlePContetxt.Articles.Remove(article);
            articlePContetxt.SaveChanges();
        }
    }
}
