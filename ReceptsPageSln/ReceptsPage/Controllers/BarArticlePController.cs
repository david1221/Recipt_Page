using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using ReceptsPage.Interfaces;
using ReceptsPage.Models;
using ReceptsPage.ViewModels;

namespace ReceptsPage.Controllers
{
    public class BarArticlePController : Controller
    {
        private readonly IBarArticles _barArticlesRepozitory;
        private readonly IArticleAndBar _allArticlesRepozitory;
        public BarArticlePController(IBarArticles articlesRepozitory, IArticleAndBar allRepozitory)
        {
            _barArticlesRepozitory = articlesRepozitory;
            _allArticlesRepozitory = allRepozitory;

        }
        static byte[] imgBar; // for change image save the field ImgBar
        public IActionResult Categories(int id, int? page)     //IQuryable<BarArticleP> get a list BarArticleP of category
        {
            if (id!=9)
            {
                CategoriesBarArticlesView catedories = new CategoriesBarArticlesView()
                {
                    BarCategoryByIdSingle = _barArticlesRepozitory.BarCategoryByIdSingle(id),
                    BarArticlesRepozitory = _barArticlesRepozitory.BarCategoryById(id).ToPagedList(page ?? 1, 8)
                };
                return View(catedories);

            }
            else
            {
                return RedirectToAction("PageArticleImage", "BarArticleP");
            }

        }
        public IActionResult SinglePageBarArticleP(int id)          //Single Page one BarArticle
        {
            BarArticleP model = id == default ? new BarArticleP() : _barArticlesRepozitory.BarGetArticlePById(id);
            return View(model);
        }
        public IActionResult PageArticleImage(int? page)          //Single Page one BarArticle
        {
            ImageArticlePage catedories = new ImageArticlePage()
            {
                ArticleImage = _allArticlesRepozitory.ArticleImage().ToPagedList(page ?? 1, 18),
               // barArticleImage= _allArticlesRepozitory.BarArticleImage().ToPagedList(page ?? 1, 18)
            };
            return View(catedories);
           
           
        }

        public IActionResult AddBarArticle(int id)
        {
            BarArticleP model = id == default ? new BarArticleP() : _barArticlesRepozitory.BarGetArticlePById(id);


            var selectList = new List<BarCategory>();       //for Selectlist in cshtml file
            foreach (var item in _barArticlesRepozitory.BarCategories())
            {
                selectList.Add(new BarCategory() { Name = item.Name, BarCategoryId = item.BarCategoryId });
            }
            ViewBag.Category = selectList;
            return View(model);
        }
        /// <summary>
        /// get method for ArticlesEdit(int id) 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ArticlesEdit(int id)    //get method for Change barArticleP
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            BarArticleP model = id == default ? new BarArticleP() : _barArticlesRepozitory.BarGetArticlePById((int)id);
            var a = new List<SubCategory>();
            foreach (var item in _barArticlesRepozitory.BarCategories())
            {
                a.Add(new SubCategory() { Name = item.Name, SubCategoryId = item.BarCategoryId });
            }
            ViewBag.Category = a;
            var selected = model.BarCategory.Name;
            if (selected != null)
            {
                model.BarCategory.Name = selected;
            }
            else model.BarCategory.Name = "all";
            //List<ArticleP> imglist = new List<ArticleP>(articlesRepozitory.GetArticles().ToArray().Length);
            //foreach (var item in imglist)
            //{
            //    img = item.ImgGeneral;
            imgBar = model.ImgGeneral;
            //}
            return View(model);

        }

        /// <summary>
        /// Post method for ArticlesEdit(int id)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ArticlesEdit(BarArticleP model, List<IFormFile> image)   //get method for Change barArticleP
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
                    else
                    {
                        if (model.BarArticleId != default)
                        {
                            model.ImgGeneral = imgBar;
                        }
                    }
                }

                _barArticlesRepozitory.SaveBarArticle(model);
                return RedirectToAction("Index", "Articles");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ArticlesDelete(int id)
        {
            BarArticleP model = _barArticlesRepozitory.BarGetArticlePById(id);
            return View(model);
            //articlesRepozitory.DeleteArticle(new ArticleP() { ArticleId = id });
            //return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ArticlesPostDelete(int id)
        {
            _barArticlesRepozitory.DeleteBarArticle(new BarArticleP() { BarArticleId = id });
            return RedirectToAction("Index", "Articles");
        }
    }
}