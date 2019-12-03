using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data.Interfaces
{
   public interface ICarsCategory
    {
        IEnumerable<Category> Allcategories { get; }
    }
}
