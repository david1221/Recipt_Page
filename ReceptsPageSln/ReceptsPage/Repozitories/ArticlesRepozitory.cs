﻿using Microsoft.EntityFrameworkCore;
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
            return _articlePContetxt.Articles.OrderByDescending(x => x.DateAdded.Value).Include(x=>x.SubCategory);

        }
        //public IQueryable<Category> GetArticlesBySession(int a)
        //{
        //    return articlePContetxt.Categories.ToList.Property(x=>x.Name).Where(x => x.CategoryId == a).Select(x => x.Name).ToArray();

        //}
        public IQueryable<ArticleP> SubCategoryById(int id)
        {
            
                return _articlePContetxt.Articles.Where(x=>x.SubCategoryId==id).Where(m => m.ImgGeneral != null).OrderByDescending(x => x.DateAdded).Include(i=>i.SubCategory);
        }


       public string SubCategoryByIdSingle(int id)
        {
            try
            {
                return _articlePContetxt.SubCategories.Where(x => x.SubCategoryId == id).FirstOrDefault().Name;
                            }
            catch (Exception )
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
            
            {
                return _articlePContetxt.Articles.Include(i=>i.SubCategory).FirstOrDefault(x => x.ArticleId == id);
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
            _articlePContetxt.Articles.Remove(article);
            _articlePContetxt.SaveChanges();
        }
    }
}
