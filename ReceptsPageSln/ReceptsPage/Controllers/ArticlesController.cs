using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceptsPage.Models;

namespace ReceptsPage.Controllers
{
    public class ArticlesController : Controller
    {
        
        private readonly ArticlesRepozitory articlesRepozitory;
        public ArticlesController(ArticlesRepozitory articlesRepozitory)
        {
            this.articlesRepozitory = articlesRepozitory;
        }
        public IActionResult Index()
        {
            var model = articlesRepozitory.GetArticles();
            return View(model);
        }
        //public IActionResult Sessions()
        //{   
        //    var model = articlesRepozitory.GetArticles().Where(x=>x.TagsArticles.amanorya==1);
        //    return View(model);
        //}
        public IActionResult SinglePage(int id)
        {
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById(id);

            return View(model);
        }
        public IActionResult AddArticle(int id)

        {
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById(id);
            
            return View(model);
        }
        public IActionResult ArticlesEdit(int id)
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById(id);
            
            return View(model);
        }
        [HttpPost] //в POST-версии метода сохраняем/обновляем запись в БД
        public IActionResult ArticlesEdit(ArticleP model)
        {
            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
                articlesRepozitory.SaveArticle(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost] //т.к. удаление статьи изменяет состояние приложения, нельзя использовать метод GET
        public IActionResult ArticlesDelete(int id)
        {
            articlesRepozitory.DeleteArticle(new ArticleP() { ArticleId = id });
            return RedirectToAction("Index");
        }
    }
}