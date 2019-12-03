using ShopCar.Data.Interfaces;
using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data.Mocks
{
    public class MockCars : IAllCars
    { private readonly ICarsCategory _carsCategory = new MockCategory();
        public IEnumerable<Car> Cars {
            get {
                return new List<Car> {
                    new Car {
                        Name ="Tesla Model S",
                        ShortDesc="Արագ ավտոմեքենա",
                        LongDesc="Գեղեցիկ ,արագ և շատ անաղմուկ ավտոմեքենա",
                        Img="/img/telsaS.jpg",
                        Price=4500,
                        Isfavourite=true,
                        Available=true,
                        Category=_carsCategory.Allcategories.First()
                    },
                    new Car {
                        Name ="Ford Fiesta",
                        ShortDesc="Հարմար և անամղմուկ",
                        LongDesc="Գեղեցիկ ,արագ և շատ անաղմուկ ավտոմեքենա հարմար քաղաքային երթևեկության համար",
                        Img="/img/ford.jpg",
                        Price=5500,
                        Isfavourite=true,
                        Available=true,
                        Category=_carsCategory.Allcategories.Last()
                    },
                    new Car {
                        Name ="BMW M3",
                        ShortDesc="Ուժեղ և վայրենի",
                        LongDesc="Հարմար ավտոմեքենա քաղաքային երթևեկության համար համար",
                        Img="/img/bmw.jpg",
                        Price=4000,
                        Isfavourite=false,
                        Available=false,
                        Category=_carsCategory.Allcategories.Last()
                    },
                    new Car {
                        Name ="Nissan Leaf",
                        ShortDesc="Անձայն և էկոնոմ",
                        LongDesc="Հարմար ավտոմեքենա քաղաքային երթևեկության համար համար",
                        Img="/img/nissan.jpg",
                        Price=6400,
                        Isfavourite=true,
                        Available=true,
                        Category=_carsCategory.Allcategories.First()
                    },
                    new Car {
                        Name ="Mersedes C class",
                        ShortDesc="Հարմար և մեծ",
                        LongDesc="Հարմար ավտոմեքենա քաղաքային երթևեկության համար համար",
                        Img="/img/mersedes.jpg",
                        Price=3900,
                        Isfavourite=false,
                        Available=true,
                        Category=_carsCategory.Allcategories.Last()
                    },

                  };
                 }

        }
        public IEnumerable<Car> GetFavCars { get ; set ; }

        public Car GetObjectCar(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
