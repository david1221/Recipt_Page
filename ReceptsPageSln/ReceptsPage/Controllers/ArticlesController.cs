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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ReceptsPage.ModelIdentity;
using Microsoft.AspNetCore.Hosting;
using ReceptsPage.Models.Comments;
using ReceptsPage.CommentViewModels;


namespace ReceptsPage.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IGetArticles articlesRepozitory;
        private readonly UserManager<AppUser> userManager;
        private readonly IHostingEnvironment iHostingEnvironment;
        // private readonly IGetComments _commentsRepoziory;

        public ArticlesController(IGetArticles articlesRepozitory, UserManager<AppUser> userManager,/* IGetComments GetComments*/ IHostingEnvironment IHostingEnvironment)
        {
            this.articlesRepozitory = articlesRepozitory;
            this.userManager = userManager;
            iHostingEnvironment = IHostingEnvironment;
            //_commentsRepoziory = GetComments;
        }

        /// <summary>
        /// Index PAge
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult Index(int? page)
        {
            var articles = articlesRepozitory.GetArticles().Result
            .Where(x => x.AdminConfirm == true)
             .AsQueryable<ArticleP>();
            DateTime time2 = DateTime.Now;
            IndexSlideArticles indexSlide = new IndexSlideArticles()
            {
                GetArticles = articles.ToPagedList(page ?? 1, 8),
                GetArticlesSlide = articles.Where(x => x.IfFavorite == true).Take(5)
            };
            // ViewBag.imgPath = Path.Combine(iHostingEnvironment.WebRootPath, @"images\default.jpg");
            return View(indexSlide);
        }

        /// <summary>
        /// articles page added from user
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public IActionResult IndexNonConfirm(int? page)
        {
            IndexSlideArticles indexSlide = new IndexSlideArticles
            {
                GetArticles = articlesRepozitory.GetArticlesByAdmin().Result
                .Where(x => x.AdminConfirm == false)
                .AsQueryable<ArticleP>()
                .ToPagedList(page ?? 1, 8),
                GetArticlesSlide = articlesRepozitory.GetArticlesByAdmin().Result
                .Where(x => x.ImgGeneral != null)
                .AsQueryable<ArticleP>()
            };

            return View(indexSlide);
        }
        /// <summary>
        /// Category Page for Article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult Categories(int id, int? page)
        {
            CategoriesArticlesView cat = new CategoriesArticlesView
            {
                //ViewBag.Name = articlesRepozitory.SubCategoryByIdSingle(id);

                articlesRepozitory = articlesRepozitory.SubCategoryById(id).Where(x => x.AdminConfirm == true).ToPagedList(page ?? 1, 8),
                SubCategoryByIdSingle = articlesRepozitory.SubCategoryByIdSingle(id)
            };
            return View(cat);
        }
        /// <summary>
        /// Single Page Article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult SinglePage(int id)
        {
            try
            {
                ArticleP model = articlesRepozitory.GetArticlePById(id);
                if (model != null)
                {
                    if (User.IsInRole("user") || model.AdminConfirm == true || model != null)
                    {
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Փնտրված բաղադրատոմսը գոյություն չունի";
                        return View("NotFound");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Փնտրված բաղադրատոմսը գոյություն չունի";
                    return View("NotFound");
                }

                // singlePageVieModel.mainComments = _commentsRepoziory.GetAllMainComments(model.ArticleId);


            }
            catch (Exception)
            {
                ;
            }

            return View("NotFound");

        }
        /// <summary>
        /// Add Article Get method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        public IActionResult AddArticle(int id)
        {

            ArticleP model = id == default ? new ArticleP() : articlesRepozitory.GetArticlePById(id);
            var selectList = new List<SubCategory>();
            foreach (var item in articlesRepozitory.SubCategories())
            {
                selectList.Add(new SubCategory() { Name = item.Name, SubCategoryId = item.SubCategoryId });
            }
            ViewBag.Category = selectList;

            return View(model);
        }
        /// <summary>
        /// Add article Post method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <param name="model1"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddArticle(ArticleP model, List<IFormFile> image)
        {
            if (ModelState.IsValid)
            {
                model.DateAdded = DateTime.Now;
                model.AppUser = userManager.FindByNameAsync(User.Identity.Name).Result;
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
                    //else
                    //{
                    //    var imgPath = Path.Combine(iHostingEnvironment.WebRootPath, @"images\default.jpg");
                    //    byte[] buffer = null;
                    //    using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                    //    {
                    //        buffer = new byte[fs.Length];
                    //        fs.Read(buffer, 0, (int)fs.Length);
                    //        model.ImgGeneral = buffer;
                    //    }
                    //}
                }

                articlesRepozitory.SaveArticle(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// Edit Article Get method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize(Roles = "admin,user")]
        public IActionResult ArticlesEdit(int id, string userName)
        {
            if ((User.IsInRole("admin") || User.Identity.Name == userName) && (articlesRepozitory.GetArticlesByUser().FirstOrDefault(u => u.Email == User.Identity.Name) == articlesRepozitory.GetArticlePById(id).AppUser) || (User.IsInRole("admin")))
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
                    else model.SubCategory.Name = "Այլ";
                }
                catch (Exception)
                { }

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Articles");
            }

        }
        /// <summary>
        /// Edit Article Post method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <param name="model1"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> ArticlesEdit(ArticleP model, List<IFormFile> image, string userName, string ArticleUser, string imageNon)
        {
            if (User.IsInRole("admin") || User.Identity.Name == userName)
            {
                if (ModelState.IsValid)
                {
                    model.DateAdded = DateTime.Now;
                    if (User.IsInRole("admin"))
                    {
                        AppUser user = userManager.FindByNameAsync(ArticleUser).Result;
                        if (user != null)
                        {
                            model.AppUser = user;
                        }
                    }
                    else
                    {
                        model.AppUser = userManager.FindByNameAsync(User.Identity.Name).Result;

                    }

                    if (image != null && imageNon != "on")
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

                        //else
                        //{
                        //    var imgPath = Path.Combine(iHostingEnvironment.WebRootPath, @"images\default.jpg");
                        //    byte[] buffer = null;
                        //    using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                        //    {
                        //        buffer = new byte[fs.Length];
                        //        fs.Read(buffer, 0, (int)fs.Length);
                        //        model.ImgGeneral = buffer;
                        //    }
                        //}
                    }
                    else
                    {
                        model.ImgGeneral = null;
                    }
                    articlesRepozitory.SaveArticle(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Articles");
            }


        }
        /// <summary>
        /// Confirm Article By Admin
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <param name="model1"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult ArticlesConfirmByAdmin(int id)
        {
            if (ModelState.IsValid)
            {
                ArticleP model = articlesRepozitory.GetArticlePById(id);
                model.AdminConfirm = true;
                articlesRepozitory.SaveArticle(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Articles");
        }


        /// <summary>
        /// Delete Article Get method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ArticlesDelete(int id)
        {
            ArticleP model = articlesRepozitory.GetArticlePById(id);
            return View(model);

        }
        /// <summary>
        /// Delete Article Post method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult ArticlesPostDelete(int id)
        {
            articlesRepozitory.DeleteArticle(new ArticleP() { ArticleId = id });
            return RedirectToAction("Index");
        }
        /// <summary>
        /// About site
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {

            return View();
        }


        //[HttpPost]
        //[Authorize(Roles = "user,admin")]
        //public async Task<IActionResult> AddMainComment(SinglePageVieModel comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string id = comment.mainCommentViewModel.UserId.ToString();
        //        var user = await userManager.FindByIdAsync(id);
        //        if (User.Identity.IsAuthenticated && user.Email == User.Identity.Name)
        //        {
        //            ArticleP _articleP = articlesRepozitory.GetArticlePById(comment.mainCommentViewModel.ArticleId);
        //            MainComment mainComment = new MainComment
        //            {
        //                appUser = user,
        //                articleP = _articleP,
        //                Date = DateTime.Now,
        //                Text = comment.mainCommentViewModel.Text
        //            };
        //            _commentsRepoziory.SaveMainComments(mainComment);
        //        }
        //        return RedirectToAction("SinglePage", new { id = comment.mainCommentViewModel.ArticleId });
        //    }
        //    else
        //    {
        //        return RedirectToAction("SinglePage", new { id = comment.mainCommentViewModel.ArticleId });

        //    }
        //}
        //[HttpPost]
        //[Authorize(Roles = "user,admin")]
        //public async Task<IActionResult> AddChildComment(SinglePageVieModel comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string id = comment.ChildCommentViewModel.UserId.ToString();
        //        var user = await userManager.FindByIdAsync(id);
        //        if (User.Identity.IsAuthenticated && user.Email == User.Identity.Name)
        //        {
        //            MainComment main = _commentsRepoziory.GetCommentByMainId(comment.mainCommentViewModel.Id);
        //            ChildComment childComment = new ChildComment
        //            {
        //                mainCommentId = comment.ChildCommentViewModel.MainCommentID,
        //                Date = DateTime.Now,
        //                Text = comment.mainCommentViewModel.Text
        //            };
        //            _commentsRepoziory.SaveChildComments(childComment);
        //        }
        //        return RedirectToAction("SinglePage", new { id = comment.mainCommentViewModel.ArticleId });
        //    }
        //    else
        //    {
        //        return RedirectToAction("SinglePage", new { id = comment.mainCommentViewModel.ArticleId });

        //    }
        //}
    }
}