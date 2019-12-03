using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data
{
    public class DBObjects
    {
        public static void initial(AppDBContent content)
        {
           

               
            if (!content.Categories.Any())
            {
                content.Categories.AddRange(Categories.Select(c => c.Value));
            }
            if (!content.Cars.Any())
            {
                content.Cars.AddRange(
                     new Car
                     {
                         Name = "Tesla Model S",
                         ShortDesc = "Արագ ավտոմեքենա",
                         LongDesc = "Գեղեցիկ ,արագ և շատ անաղմուկ ավտոմեքենա",
                         Img = "/img/telsaS.jpg",
                         Price = 4500,
                         Isfavourite = true,
                         Available = true,
                         Category = Categories["Էլեկտրոմոբիլներ"]
                     },
                    new Car
                    {
                        Name = "Ford Fiesta",
                        ShortDesc = "Հարմար և անամղմուկ",
                        LongDesc = "Գեղեցիկ ,արագ և շատ անաղմուկ ավտոմեքենա հարմար քաղաքային երթևեկության համար",
                        Img = "/img/ford.jpg",
                        Price = 5500,
                        Isfavourite = true,
                        Available = true,
                        Category = Categories["Դասական ավտոմեքենաներ"]
                    },
                    new Car
                    {
                        Name = "BMW M3",
                        ShortDesc = "Ուժեղ և վայրենի",
                        LongDesc = "Հարմար ավտոմեքենա քաղաքային երթևեկության համար համար",
                        Img = "/img/bmw.jpg",
                        Price = 4000,
                        Isfavourite = false,
                        Available = false,
                        Category = Categories["Դասական ավտոմեքենաներ"]
                    },
                    new Car
                    {
                        Name = "Nissan Leaf",
                        ShortDesc = "Անձայն և էկոնոմ",
                        LongDesc = "Հարմար ավտոմեքենա քաղաքային երթևեկության համար համար",
                        Img = "/img/nissan.jpg",
                        Price = 6400,
                        Isfavourite = true,
                        Available = true,
                        Category = Categories["Էլեկտրոմոբիլներ"]
                    },
                    new Car
                    {
                        Name = "Mersedes C class",
                        ShortDesc = "Հարմար և մեծ",
                        LongDesc = "Հարմար ավտոմեքենա քաղաքային երթևեկության համար համար",
                        Img = "/img/mersedes.jpg",
                        Price = 3900,
                        Isfavourite = false,
                        Available = true,
                        Category =Categories["Դասական ավտոմեքենաներ"]
                    }
                    );
            }
            content.SaveChanges();
        }
        private static Dictionary<string, Category> category;
        public static Dictionary<string,Category> Categories
        {
            get{
                if (category==null)
                {
                    var list = new Category[]
                    {
                         new Category{CategoryName="Էլեկտրոմոբիլներ" ,Desc="Տրանսպորտային նորարար տեսակ" },
                    new Category{CategoryName="Դասական ավտոմեքենաներ" ,Desc="Վառելիքային շարժիչով մեքենաներ" }
                    };
                    category = new Dictionary<string, Category>();
                    foreach (Category el in list)
                    {
                        category.Add(el.CategoryName,el);
                    }
                }
                return category;
            }
        }

    }
}
