using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceptsPage.Interfaces;
using ReceptsPage.Models;

namespace ReceptsPage.Controllers
{
    public class BarArticlePController : Controller
    {
        private readonly IBarArticles _barArticlesRepozitory;
        public BarArticlePController(IBarArticles articlesRepozitory)
        {
           _barArticlesRepozitory = articlesRepozitory;

        }
        public IActionResult Categories(int id)
        {
            ViewBag.Name = _barArticlesRepozitory.BarCategories().Where(x=>x.BarCategoryId==id).FirstOrDefault(c=>c.BarCategoryId==id).Name;
            var model = _barArticlesRepozitory.BarCategoryById(id);

            return View(model);
        }
        public IActionResult AddArticle(int id)
        {
            BarArticleP model = id == default ? new BarArticleP() : _barArticlesRepozitory.BarGetArticlePById(id);


            var a = new List<BarCategory>();
            foreach (var item in _barArticlesRepozitory.BarCategories())
            {
                a.Add(new BarCategory() { Name = item.Name, BarCategoryId = item.BarCategoryId });
            }
            ViewBag.Category = a;

            return View(model);
        }
    }
}