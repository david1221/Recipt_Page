using ReceptsPage.Models;
using ReceptsPage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.Interfaces
{
  public  interface IArticleAndBar

    {
        // IEnumerable<BarArticleP> BarArticleImage();
        Task<IList<ArticleP>> ArticleImage();
        Task<IList<BarArticleP>> BarArticleImage();
        IEnumerable<ImageAll> ImagesAll();
    }
}
