using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Interfaces
{
    public interface IGetArticles
    {
        IQueryable<ArticleP> SubCategoryById(int id);
        Task<IList<ArticleP>> GetArticlesByAdmin();
        Task<IList<ArticleP>> GetArticles();
        IList<ArticleP> GetArticlesFromCacheRep();
        IQueryable<SubCategory> SubCategories();

        ArticleP GetArticlePById(int id);

        int SaveArticle(ArticleP articleP);

        void DeleteArticle(ArticleP article);
        string SubCategoryByIdSingle(int id);
        IQueryable<AppUser> GetArticlesByUser();
        IQueryable<AppUser> GetArticlesByUserWithoutSubCategory();
        Task<IList<ArticleP>> GetArticlesWithoutSubCategory();

    }
}
