
using PagedList.Core;
using ReceptsPage.Interfaces;
using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ViewModels
{
    public class CategoriesArticlesView
    {
      public IPagedList<ArticleP> articlesRepozitory;
        public string SubCategoryByIdSingle;
    }
}
