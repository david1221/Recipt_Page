using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.Models.VideoModel;
using ReceptsPage.Repozitories;
using ReceptsPage.ViewModels;


namespace ReceptsPage.Controllers
{
    public class BarArticlePController : Controller
    {
        private readonly IBarArticles _barArticlesRepozitory;
        private readonly IArticleAndBar _allArticlesRepozitory;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHostingEnvironment _iHostingEnvironment;


        public BarArticlePController(IBarArticles articlesRepozitory, IArticleAndBar allRepozitory, UserManager<AppUser> userManager, IHostingEnvironment IHostingEnvironment)
        {
            _barArticlesRepozitory = articlesRepozitory;
            _allArticlesRepozitory = allRepozitory;
            _iHostingEnvironment = IHostingEnvironment;
            _userManager = userManager;

        }

        public IActionResult Categories(int id, int? page)     //IQuryable<BarArticleP> get a list BarArticleP of category
        {

            CategoriesBarArticlesView catedories = new CategoriesBarArticlesView()
            {
                BarCategoryByIdSingle = _barArticlesRepozitory.BarCategoryByIdSingle(id),
                BarArticlesRepozitory = _barArticlesRepozitory.BarCategoryById(id).Where(x => x.AdminConfirm == true).ToPagedList(page ?? 1, 8)
            };
            return View(catedories);



        }
        public IActionResult SinglePageBarArticleP(int id)          //Single Page one BarArticle
        {
            try
            {
                BarArticleP model = _barArticlesRepozitory.BarGetArticlePById(id);
                if (User.IsInRole("user") || model.AdminConfirm == true)
                {
                    return View(model);
                }
                else return NotFound();

            }
            catch (Exception)
            {

                RedirectToAction("Index", "Articles");
            }
            return NotFound();
        }


        public IActionResult PageArticleImage(int? page)
        {

            var allImages = _allArticlesRepozitory.ImagesAll();
            var model1 = allImages.AsQueryable();
            ;

            var model = model1.ToPagedList(page ?? 1, 9);

            // barArticleImage= _allArticlesRepozitory.BarArticleImage().ToPagedList(page ?? 1, 18)


            return View(model);


        }

        public IActionResult VideoSinglePage(int id)
        {
            var model = _barArticlesRepozitory.GetBarVideoArticles().FirstOrDefault(v => v.Id == id);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AddBarVideoArticle()
        {
            return View();
        }



        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddBarVideoArticle(VideoArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UniqeFileName = null;
                if (model.image != null)
                {
                    string uploadFolder = Path.Combine(_iHostingEnvironment.WebRootPath, "images\\VideoImages");
                    UniqeFileName = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                    string filePath = Path.Combine(uploadFolder, UniqeFileName);
                    model.image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                VideoModelA newVideoModel = new VideoModelA
                {
                    Description = model.Description,
                    date = DateTime.Now,
                    image = UniqeFileName,
                    usl_src = model.YoutubeFrame
                };
                _barArticlesRepozitory.SaveBarVideoArticle(newVideoModel);
                return RedirectToAction("Index", "Articles");
            }
            return View();
        }
        public IActionResult PageArticleVideo(int? page)
        {
            var model1 = _barArticlesRepozitory.GetBarVideoArticles();
            var model = model1.ToPagedList(page ?? 1, 6);

            // barArticleImage= _allArticlesRepozitory.BarArticleImage().ToPagedList(page ?? 1, 18)


            return View(model);


        }
        [HttpGet]
        [Authorize(Roles = "user,admin")]
        public IActionResult AddBarArticle(int id)
        {
            BarArticleP barArticleP = id == default ? new BarArticleP() : _barArticlesRepozitory.BarGetArticlePById(id);
            AddBarArticleViewModel model = new AddBarArticleViewModel();

            var selectList = new List<BarCategory>();       //for Selectlist in cshtml file
            foreach (var item in _barArticlesRepozitory.BarCategories())
            {
                if (item.BarCategoryId != 10)
                {
                    selectList.Add(new BarCategory() { Name = item.Name, BarCategoryId = item.BarCategoryId });
                }

            }
            model.barCategories = selectList;
            model.barArtcileP = barArticleP;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> AddBarArticle(AddBarArticleViewModel model, List<IFormFile> image)
        {
            if (ModelState.IsValid)
            {
                model.barArtcileP.DateAdded = DateTime.Now;
                model.barArtcileP.BarCategoryId = model.barArtcileP.BarCategoryId == 0 ? 12 : model.barArtcileP.BarCategoryId;


                model.barArtcileP.AppUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
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
                                    model.barArtcileP.ImgGeneral = stream.ToArray();
                                }
                            }
                        }
                    }
                }

                _barArticlesRepozitory.SaveBarArticle(model.barArtcileP);
                return RedirectToAction("Index", "Articles");
            }
            var selectList = new List<BarCategory>();       //for Selectlist in cshtml file
            foreach (var item in _barArticlesRepozitory.BarCategories())
            {
                if (item.BarCategoryId != 10)
                {
                    selectList.Add(new BarCategory() { Name = item.Name, BarCategoryId = item.BarCategoryId });
                }

            }
            model.barCategories = selectList;

            return View(model);
        }

        /// <summary>
        /// get method for ArticlesEdit(int id) 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin,user")]
        public IActionResult BarArticlesEdit(int id, string userName)    //get method for Change barArticleP
        {


            if ((User.IsInRole("admin") || User.Identity.Name == userName) && (_barArticlesRepozitory.GetBarArticlesByUser().FirstOrDefault(u => u.Email == User.Identity.Name) == _barArticlesRepozitory.BarGetArticlePById(id).AppUser) || (User.IsInRole("admin")))
            {
                BarArticleP model = _barArticlesRepozitory.BarGetArticlePById((int)id);
                var a = new List<BarCategory>();
                foreach (var item in _barArticlesRepozitory.BarCategories())
                {
                    if (item.BarCategoryId != 10)
                    {
                        a.Add(new BarCategory() { Name = item.Name, BarCategoryId = item.BarCategoryId });
                    }

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

                //}
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Articles");
            }



        }

        /// <summary>
        /// Post method for BarArticlesEdit(int id)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> BarArticlesEdit(BarArticleP model, List<IFormFile> image, string userName, string ArticleUser, string imageNon)
        {
            if (User.IsInRole("admin") || User.Identity.Name == userName)
            {
                if (ModelState.IsValid)
                {
                    model.DateAdded = DateTime.Now;
                    if (User.IsInRole("admin"))
                    {
                        var user = _userManager.FindByNameAsync(ArticleUser).Result;

                    }
                    else
                    {
                        model.AppUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
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


                    }
                    else
                    {
                        model.ImgGeneral = null;
                    }
                    _barArticlesRepozitory.SaveBarArticle(model);
                    return RedirectToAction("Index", "Articles");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Articles");
            }

        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult barArticlesConfirmByAdmin(int id)
        {

            BarArticleP model = _barArticlesRepozitory.BarGetArticlePById(id);
            model.AdminConfirm = true;
            _barArticlesRepozitory.SaveBarArticle(model);
            return RedirectToAction("Index", "Articles");

        }

        [Authorize(Roles = "admin")]
        public IActionResult IndexNonConfirm(int? page)
        {

            var model = _barArticlesRepozitory.GetBarArticles().Where(x => x.AdminConfirm == false).ToPagedList(page ?? 1, 8);


            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ArticlesDelete(int id)
        {
            BarArticleP model = _barArticlesRepozitory.BarGetArticlePById(id);
            return View(model);
            //articlesRepozitory.DeleteArticle(new ArticleP() { ArticleId = id });
            //return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult BarArticlesPostDelete(int id)
        {
            _barArticlesRepozitory.DeleteBarArticle(new BarArticleP() { BarArticleId = id });
            return RedirectToAction("Index", "Articles");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult VideoArticlesPostDelete(int id)
        {
            _barArticlesRepozitory.DeleteVideoBarArticle(new VideoModelA() { Id = id });
            return RedirectToAction("Index", "Articles");
        }


    }
}