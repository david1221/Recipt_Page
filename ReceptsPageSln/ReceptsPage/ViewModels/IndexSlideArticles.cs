using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList.Core;
using ReceptsPage.Interfaces;


namespace ReceptsPage.ViewModels
{
    public class IndexSlideArticles
    {
        public IEnumerable<ArticleP> GetArticlesSlide { get; set; }
        public IPagedList<ArticleP> GetArticles { get; set; }

     //   public IGetArticles articlesRepozitory{get;set;}
        public string By { get; set; }

    }
}
