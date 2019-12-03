using ShopCar.Data.Interfaces;
using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data.Repozitory
{
    public class CategoryRepozitory : ICarsCategory
    {
        private readonly AppDBContent _appDBContent;
        public CategoryRepozitory(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }
        public IEnumerable<Category> Allcategories => _appDBContent.Categories;
    }
}
