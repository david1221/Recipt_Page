﻿using System;
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
        static byte[] img;

        public IActionResult Index(int? page)
        {
            IndexSlideArticles indexSlide = new IndexSlideArticles();
            indexSlide.articlesRepozitory = articlesRepozitory;
            indexSlide.GetArticles = articlesRepozitory.GetArticles().Where(x => x.ImgGeneral != null).ToPagedList(page ?? 1, 8);
            indexSlide.GetArticlesSlide = articlesRepozitory.GetArticles().Where(x => x.ImgGeneral != null);



            return View(indexSlide);
        }
        public IActionResult Categories(int id, int? page)
        {
            CategoriesArticlesView cat = new CategoriesArticlesView();
            //ViewBag.Name = articlesRepozitory.SubCategoryByIdSingle(id);

            cat.articlesRepozitory = articlesRepozitory.SubCategoryById(id).ToPagedList(page ?? 1, 8);
            cat.SubCategoryByIdSingle = articlesRepozitory.SubCategoryByIdSingle(id);
            return View(cat);
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


            var a = new List<SubCategory>();
            foreach (var item in articlesRepozitory.SubCategories())
            {
                a.Add(new SubCategory() { Name = item.Name, SubCategoryId = item.SubCategoryId });
            }
            ViewBag.Category = a;

            return View(model);
        }





        public IActionResult ArticlesEdit(int id)
        {
            //либо создаем новую статью, либо выбираем существующую и передаем в качестве модели в представление
            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById((int)id);
            var a = new List<SubCategory>();
            foreach (var item in articlesRepozitory.SubCategories())
            {
                a.Add(new SubCategory() { Name = item.Name, SubCategoryId = item.SubCategoryId });
            }
            ViewBag.Category = a;
            var selected = model.SubCategory.Name;
            if (selected != null)
            {
                model.SubCategory.Name = selected;
            }
            else model.SubCategory.Name = "all";
            //List<ArticleP> imglist = new List<ArticleP>(articlesRepozitory.GetArticles().ToArray().Length);
            //foreach (var item in imglist)
            //{
            //    img = item.ImgGeneral;
            img = model.ImgGeneral;
            //}
            return View(model);

        }
        [HttpPost] //в POST-версии метода сохраняем/обновляем запись в БД
        public async Task<IActionResult> ArticlesEdit(ArticleP model, List<IFormFile> image)
        {

            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
                if (model.SubCategoryId == null)
                {
                    model.SubCategoryId = 17;
                }
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
                        if (model.ArticleId != default)
                        {
                            model.ImgGeneral = img;
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