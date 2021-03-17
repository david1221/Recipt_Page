using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.ViewModels;

namespace ReceptsPage.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGetArticles articlesRepozitory;
        private readonly UserManager<AppUser> userManager;

        // private readonly IGetComments _commentsRepoziory;

        public AdminController(IGetArticles articlesRepozitory, UserManager<AppUser> userManager/* IGetComments GetComments*/ )
        {
            this.articlesRepozitory = articlesRepozitory;
            this.userManager = userManager;

            //_commentsRepoziory = GetComments;
        }

        public IActionResult Index(int? page)
        {
            var articles = articlesRepozitory.GetArticlesByAdmin().Result
            .AsQueryable<ArticleP>();
            IndexSlideArticles indexSlide = new IndexSlideArticles()
            {
                GetArticles = articles
                .ToPagedList(page ?? 1, 8),
                GetArticlesSlide = articles
                 .Where(x => x.IfFavorite == true)
                 .Take(5)
            };
          

            // ViewBag.imgPath = Path.Combine(iHostingEnvironment.WebRootPath, @"images\default.jpg");

            return View(indexSlide);

        }
    }
}
