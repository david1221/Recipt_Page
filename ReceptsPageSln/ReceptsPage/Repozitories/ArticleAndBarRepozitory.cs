using ReceptsPage.Interfaces;
using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Repozitories
{
    public class ArticleAndBarRepozitory : IArticleAndBar
    {

        private readonly ArticlePContetxt _articlePContetxt;
        public ArticleAndBarRepozitory(ArticlePContetxt articlePContetxt)
        {
            _articlePContetxt = articlePContetxt;
        }
        public IEnumerable<ArticleP> ArticleImage()
        {
            return _articlePContetxt.Articles.Where(x=>x.ImgGeneral!=null).OrderBy(x=>x.DateAdded);
        }

        //public IEnumerable<BarArticleP> BarArticleImage()
        //{
        //    return _articlePContetxt.BarArticles.Where(x => x.ImgGeneral != null).OrderBy(x => x.DateAdded);
        //}
      
    }
}
