using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.Models.VideoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Interfaces
{
    public interface IBarArticles
    {
        IQueryable<BarArticleP> GetBarArticles();
        BarArticleP BarGetArticlePById(int id);
        IQueryable<BarArticleP> BarCategoryById(int id);
        IQueryable<BarCategory> BarCategories();
        void DeleteBarArticle(BarArticleP BarArticle);
        int SaveBarArticle(BarArticleP barArticleP);
        string BarCategoryByIdSingle(int id);
        IQueryable<AppUser> GetBarArticlesByUser();

        int SaveBarVideoArticle(VideoModelA videoModelA);
        IQueryable<VideoModelA> GetBarVideoArticles();
        void DeleteVideoBarArticle(VideoModelA model);
        IQueryable<AppUser> GetBarArticlesByUserWithoutBarCategory();
        IQueryable<BarArticleP> GetBarArticlesWithoutBarCategory();
    }
}
