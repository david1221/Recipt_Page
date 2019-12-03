using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReceptsPage.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using PagedList.Core;
using ReceptsPage.ViewModels;

namespace ReceptsPage.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly ArticlesRepozitory articlesRepozitory;
        public ArticlesController(ArticlesRepozitory articlesRepozitory)
        {
            this.articlesRepozitory = articlesRepozitory;

        }

        public IActionResult Index(int? page)
        {
            IndexSlideArticles indexSlide = new IndexSlideArticles();
            indexSlide.GetArticles= articlesRepozitory.GetArticles().Where(x => x.ImgGeneral != null).ToPagedList(page ?? 1, 8);
            indexSlide.GetArticlesSlide = articlesRepozitory.GetArticles().Where(x => x.ImgGeneral != null);
            ViewBag.Name = articlesRepozitory;
           
            
            return View(indexSlide);
        }
        public IActionResult Categories(int id)
        {
            ViewBag.Name = articlesRepozitory.SubCategoryByIdSingle(id);

            var model = articlesRepozitory.SubCategoryById(id);
           
            return View(model);
        }
        //public IActionResult Sessions()
        //{   
        //    var model = articlesRepozitory.GetArticles().Where(x=>x.TagsArticles.amanorya==1);
        //    return View(model);
        //}
        public IActionResult SinglePage(int id  )
        {
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById(id);

            return View(model);
        }
        public IActionResult AddArticle(int id)
        {
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById(id);
           
            
           var a = new List<SubCategory>();
            foreach (var item in articlesRepozitory.SubCategories())
            {
              a.Add( new SubCategory() { Name=item.Name,SubCategoryId=item.SubCategoryId });
            }
            ViewBag.Category = a;

            return View(model);
        }





        public IActionResult ArticlesEdit(int id)
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById((int)id);

            return View(model);
        }
        [HttpPost] //в POST-версии метода сохраняем/обновляем запись в БД
        public async Task<IActionResult> ArticlesEdit(ArticleP model,List<IFormFile> image)
        {
           
            if (ModelState.IsValid)
            {       
                
                model.DateAdded = DateTime.Now;
                if (model.SubCategoryId==null)
                {
                    model.SubCategoryId = 17;
                }
                if (image!=null)
                {
                    foreach (var item in image)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                model.ImgGeneral = stream.ToArray();
                            }

                        }
                    }
                }
                
               
               
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