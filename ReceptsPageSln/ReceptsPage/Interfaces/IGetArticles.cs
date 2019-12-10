﻿using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Interfaces
{
  public interface IGetArticles
    {
         IQueryable<ArticleP> SubCategoryById(int id);

        IQueryable<ArticleP> GetArticles();

        string SubCategoryByIdSingle(int id);

        IQueryable<SubCategory> SubCategories();

        ArticleP GetArticlePById(int id);

        int SaveArticle(ArticleP articleP);

        void DeleteArticle(ArticleP article);

    }
}