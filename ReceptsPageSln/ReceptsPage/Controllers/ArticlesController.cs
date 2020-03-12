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
using ReceptsPage.Interfaces;
using System.Diagnostics;

namespace ReceptsPage.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly IGetArticles articlesRepozitory;
        public ArticlesController(IGetArticles articlesRepozitory)
        {
            this.articlesRepozitory = articlesRepozitory;

        }
   

        public IActionResult Index(int? page)
        {
            IndexSlideArticles indexSlide = new IndexSlideArticles
            {
                articlesRepozitory = articlesRepozitory,
                GetArticles = articlesRepozitory.GetArticles().Where(x => x.ImgGeneral != null).ToPagedList(page ?? 1, 8),
                GetArticlesSlide = articlesRepozitory.GetArticles().Where(x => x.ImgGeneral != null)
            };



            return View(indexSlide);
        }
        public IActionResult Categories(int id, int? page)
        {
            CategoriesArticlesView cat = new CategoriesArticlesView
            {
                //ViewBag.Name = articlesRepozitory.SubCategoryByIdSingle(id);

                articlesRepozitory = articlesRepozitory.SubCategoryById(id).ToPagedList(page ?? 1, 8),
                SubCategoryByIdSingle = articlesRepozitory.SubCategoryByIdSingle(id)
            };
            return View(cat);
        }
       
        public IActionResult SinglePage(int id)
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
                a.Add(new SubCategory() { Name = item.Name, SubCategoryId = item.SubCategoryId });
            }
            ViewBag.Category = a;

            return View(model);
        }




        public IActionResult About()
        {
            return View();
        }
        public IActionResult ArticlesEdit(int id)
        {
           
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById((int)id);
            var a = new List<SubCategory>();
            foreach (var item in articlesRepozitory.SubCategories())
            {
                a.Add(new SubCategory() { Name = item.Name, SubCategoryId = item.SubCategoryId });
            }
            ViewBag.Category = a;
            try
            {
                var selected = model.SubCategory.Name;
                if (selected != null)
                {
                    model.SubCategory.Name = selected;
                }
                else model.SubCategory.Name = "all";
            }
            catch (Exception)
            {

               
            }
            
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ArticlesEdit(ArticleP model, List<IFormFile> image, byte[] model1)
        {

            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
               
                if (image != null)
                {
                    if (image.Count > 0)
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
                   
                }
                articlesRepozitory.SaveArticle(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ArticlesDelete(int id)
        {
            ArticleP model = articlesRepozitory.GetArticlePById(id);
            return View(model);
           
        }
        [HttpPost]
        public IActionResult ArticlesPostDelete(int id)
        {
            articlesRepozitory.DeleteArticle(new ArticleP() { ArticleId = id });
            return RedirectToAction("Index");
        }
    }
}
