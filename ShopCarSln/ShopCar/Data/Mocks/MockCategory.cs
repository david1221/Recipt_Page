using ShopCar.Data.Interfaces;
using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data.Mocks
{
    public class MockCategory : ICarsCategory
    {
        public IEnumerable<Category> Allcategories
        {
            get
            {
                return new List<Category>
                {
                    new Category{CategoryName="Էլեկտրոմոբիլներ" ,Desc="Տրանսպորտային նորարար տեսակ" },
                    new Category{CategoryName="Դասական ավտոմեքենաներ" ,Desc="Վառելիքային շարժիչով մեքենաներ" },
                };

            }
        }
    }
}
