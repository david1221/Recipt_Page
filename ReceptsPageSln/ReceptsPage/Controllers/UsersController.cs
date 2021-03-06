﻿using System;
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
using ReceptsPage.IdentityViewModels;
using ReceptsPage.Interfaces;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;
using ReceptsPage.ViewModels;
//using X.PagedList;

namespace ReceptsPage.Controllers
{

    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IGetArticles _articlesRepozitory;
        private readonly IHostingEnvironment iHostingEnvironment;
        private readonly IBarArticles _barArticlesRepozitory;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IGetArticles articlesRepozitory, IHostingEnvironment IHostingEnvironment, IBarArticles barArticlesRepozitory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _articlesRepozitory = articlesRepozitory;
            iHostingEnvironment = IHostingEnvironment;
            _barArticlesRepozitory = barArticlesRepozitory;
        }
        /// <summary>
        /// Select List User 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public IActionResult Index(int? page)
        {
            ViewBag.Count = _userManager.Users.Where(a => a.Email != null).Count();
            return View(_userManager.Users.ToPagedList(page ?? 1, 10));
        }


        /// <summary>
        /// Delete User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                try
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                }
                catch (Exception)
                {

                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Change Password in User page GET Method
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ChangePassword()
        {
            var ap = _articlesRepozitory.GetArticlesByUser();
            AppUser user = await _userManager.FindByNameAsync(ap.FirstOrDefault(u => u.Email == User.Identity.Name).Email);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        /// <summary>
        /// Change Password in User page Post MEthod
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync($"{ model.Id}");
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Articles");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Օգտատերը գտնված չէ");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Change Password for user With Admin GET Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangePasswordForUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }


        /// <summary>
        /// Change Password for user With Admin POST Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordForUser(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<AppUser>)) as IPasswordValidator<AppUser>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<AppUser>)) as IPasswordHasher<AppUser>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Օգտատերը գտնված չէ");
                }
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UserPageForChange(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            user.Articles = _articlesRepozitory.GetArticles().Result
                .Where(u => u.AppUser.Id == int.Parse(userId)).ToList();
            user.BarArticles = _barArticlesRepozitory.GetBarArticles()
                .Where(u => u.AppUser.Id == int.Parse(userId)).ToList();

            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Articles");
        }

        /// <summary>
        /// Person GET method for change 
        /// </summary>
        /// <returns></returns>

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UserPageForChange(EditUserWithAdminViewModel user, List<IFormFile> imageUser, string imageNon)
        {
            string phone = null;
            try
            {
                int pos = user.PhoneNumber.Length;
                pos -= 8;
                phone = user.PhoneNumber.Substring(pos, 8);
                phone = "+374" + phone;
            }
            catch (Exception) {; }

            AppUser appUser = await _userManager.FindByNameAsync(user.EmailUser);

            ViewBag.countArticle = _articlesRepozitory.GetArticlesWithoutSubCategory().Result
                .Where(a => a.AppUser.Id == appUser.Id).Count();
            ViewBag.countBarArticle = _barArticlesRepozitory.GetBarArticlesWithoutBarCategory()
                .Where(a => a.AppUser.Id == appUser.Id).Count();
            bool IsPhoneAlreadyRegistered = _userManager.Users.Any(item => item.PhoneNumber == phone && phone != appUser.PhoneNumber) && phone != null;
            appUser.Birthdate = user.Birthdate;
            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;

            appUser.Gender = user.Gender;
            if (imageUser != null && imageNon != "on")
            {
                if (imageUser.Count > 0)
                {
                    foreach (var item in imageUser)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                appUser.PhotoUser = stream.ToArray();
                            }
                        }
                    }
                }
            }
            else
            {
                appUser.PhotoUser = user.PhotoUser;
            }
            if (user.EmailConfirme == true)
            {
                appUser.EmailConfirmed = true;
            }
            else if (user.EmailConfirme == false)
            {
                appUser.EmailConfirmed = false;
            }
            if (user.PhoneNumberConfirm == true)
            {
                appUser.PhoneNumberConfirmed = true;
            }
            else if (user.PhoneNumberConfirm == false)
            {
                appUser.PhoneNumberConfirmed = false;
            }

            if (!IsPhoneAlreadyRegistered)
            {

                appUser.PhoneNumber = phone;
                var result = await _userManager.UpdateAsync(appUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserPageForChange", new { UserId = appUser.Id });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(appUser);
            }
            else
            {
                ViewBag.phone = "Փորձեք այլ հեռախոսահամար";
                return View(appUser);
            }

        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> Person()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Articles = _articlesRepozitory.GetArticlesWithoutSubCategory().Result
                .Where(u => u.AppUser.Email == User.Identity.Name).ToList();
            user.BarArticles = _barArticlesRepozitory.GetBarArticlesWithoutBarCategory()
                .Where(u => u.AppUser.Email == User.Identity.Name).ToList();

            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Articles");
        }

        /// <summary>
        /// User Change  Post Method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="imageUser"></param>
        /// <param name="imageNon"></param>
        /// <returns></returns>
        [Authorize(Roles = "user,admin")]
        [HttpPost]
        public async Task<IActionResult> Person(EditUserViewModel user, List<IFormFile> imageUser, string imageNon)
        {
            string phone = null;
            try
            {
                int pos = user.PhoneNumber.Length;
                pos -= 8;
                phone = user.PhoneNumber.Substring(pos, 8);
                phone = "+374" + phone;
            }
            catch (Exception) {; }

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            bool IsPhoneAlreadyRegistered = _userManager.Users.Any(item => item.PhoneNumber == phone && phone != appUser.PhoneNumber) && phone != null;
            appUser.Birthdate = user.Birthdate;
            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;

            appUser.Gender = user.Gender;
            if (imageUser != null && imageNon != "on")
            {
                if (imageUser.Count > 0)
                {
                    foreach (var item in imageUser)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                appUser.PhotoUser = stream.ToArray();
                            }
                        }
                    }
                }
            }
            else
            {
                appUser.PhotoUser = user.PhotoUser;
            }
            if (!IsPhoneAlreadyRegistered)
            {

                appUser.PhoneNumber = phone;
                var result = await _userManager.UpdateAsync(appUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Person");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(appUser);
            }
            else
            {
                ViewBag.phone = "Փորձեք այլ հեռախոսահամար";
                return View(appUser);
            }

        }

        /// <summary>
        /// Get Articles page User
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PersonArticles(int? page, string userId)
        {
            var ap = _articlesRepozitory.GetArticlesByUserWithoutSubCategory();
            AppUser appUser = null;
            if (User.IsInRole("admin"))
            {
                appUser = await _userManager.FindByIdAsync(userId);
            }
            else
            {
                appUser = await _userManager.FindByNameAsync(ap.FirstOrDefault(u => u.Email == User.Identity.Name).Email);
            }

            var model = _articlesRepozitory.GetArticles().Result
                .Where(x => x.AppUser.Id == appUser.Id)
                .AsQueryable<ArticleP>()
                .ToPagedList(page ?? 1, 10);


            //ViewBag.imgPath = Path.Combine(iHostingEnvironment.WebRootPath, @"images\default.jpg");
            return View(model);

        }
        /// <summary>
        /// Get Articles page User
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PersonBarArticles(int? page, string userId)
        {
            var ap = _barArticlesRepozitory.GetBarArticlesByUserWithoutBarCategory();
            AppUser appUser = null;
            if (User.IsInRole("admin"))
            {
                appUser = await _userManager.FindByIdAsync(userId);
            }
            else
            {
                appUser = await _userManager.FindByNameAsync(ap.FirstOrDefault(u => u.Email == User.Identity.Name).Email);
            }

            var model = _barArticlesRepozitory.GetBarArticles()
                .Where(x => x.AppUser.Id == appUser.Id).ToPagedList(page ?? 1, 10);

            //ViewBag.imgPath = Path.Combine(iHostingEnvironment.WebRootPath, @"images\default.jpg");
            return View(model);

        }

        /// <summary>
        /// Get Photo User with USer id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<FileContentResult> Photo(string userId)
        {
            // get EF Database (maybe different way in your applicaiton)

            // find the user. I am skipping validations and other checks.
            AppUser appUser = await _userManager.FindByNameAsync(userId);

            if (appUser.PhotoUser != null)
            {
                return new FileContentResult(appUser.PhotoUser, "image/jpeg");
            }
            byte[] buffer = null; string path = null;
            if (appUser.Gender == "Իգական")
            {
                path = Path.Combine(iHostingEnvironment.WebRootPath, "images/female.png");

                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                }
            }
            else
            {
                path = Path.Combine(iHostingEnvironment.WebRootPath, "images/male.png");

                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                }
            }
            return new FileContentResult(buffer, "image/jpeg");//new FileContentResult(appUser.PhotoUser, "image/jpeg");
        }

    }
}