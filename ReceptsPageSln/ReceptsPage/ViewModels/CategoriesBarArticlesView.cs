using PagedList.Core;
using ReceptsPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceptsPage.ViewModels
{
    public class CategoriesBarArticlesView
    {
        public IPagedList<BarArticleP> BarArticlesRepozitory;
        public string BarCategoryByIdSingle;
    }
}
