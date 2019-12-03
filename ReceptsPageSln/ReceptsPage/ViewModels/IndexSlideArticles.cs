using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList.Core;

namespace ReceptsPage.ViewModels
{
    public class IndexSlideArticles
    {
        public IQueryable<ArticleP> GetArticlesSlide { get; set; }
        public IPagedList<ArticleP> GetArticles { get; set; }


    }
}
