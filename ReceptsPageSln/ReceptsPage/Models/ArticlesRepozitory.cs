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
        //public IQueryable<category> GetArticlesBySession(int a)
        //{
        //    var a = articlePContetxt.Categories;
        //    return a.Find; Where(x => x.TagsArticles);

        //}

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
