using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopCar.Data.Interfaces;
using ShopCar.ViewModels;

namespace ShopCar.Controllers
{
    public class CarsController : Controller
    {
        private readonly IAllCars _allCars;
        private readonly ICarsCategory _carsCategory;
        public CarsController(IAllCars allCars,ICarsCategory carsCategory)
        {
            _allCars = allCars;
            _carsCategory = carsCategory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            CarsListViewModel cars =new CarsListViewModel();
            ViewBag.Title = "Մեքենաներ";
            cars.GetAllCars = _allCars.Cars;
            cars.CurrCategory = "Մեքենաներ";
            return View(cars);
        }
    }
}