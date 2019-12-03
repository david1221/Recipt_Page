using Microsoft.EntityFrameworkCore;
using ShopCar.Data.Interfaces;
using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data.Repozitory
{
    public class CarRepozitory : IAllCars
    {
        private readonly AppDBContent _appDBContent;
        public CarRepozitory(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }
        public IEnumerable<Car> Cars=>_appDBContent.Cars.Include(c=>c.Category);

        public IEnumerable<Car> GetFavCars => _appDBContent.Cars.Where(p => p.Isfavourite).Include(c=>c.Category);

        public Car GetObjectCar(int carId) => _appDBContent.Cars.FirstOrDefault(p => p.Id == carId);
        
    }
}
