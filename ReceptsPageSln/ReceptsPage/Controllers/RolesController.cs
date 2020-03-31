using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceptsPage.IdentityViewModels;
using ReceptsPage.ModelIdentity;

namespace ReceptsPage.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<AppRole> _roleManager;
        UserManager<AppUser> _userManager;
        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Roles() => View(_roleManager.Roles.ToList());
        public IActionResult Index() => View(_userManager.Users.ToList());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
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
            return View(name);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync($"{id}");
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        public IActionResult UserList() => View(_userManager.Users.ToList());
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditAll()
        {


            if (_userManager.Users != null)
            {
                List<UserRoleAllViewModel> models = new List<UserRoleAllViewModel>();
                UserRoleAllViewModel model;

                ViewBag.Dictionary = new Dictionary<int, bool>();
                foreach (var user in _userManager.Users.ToList())
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    var allUser = _userManager.Users.ToList();
                    model = new UserRoleAllViewModel()
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserRoles = userRoles,
                        AllRoles = allRoles,
                        AllUsers = allUser
                    };
                    try
                    {
                        models.Add(model);
                    }
                    catch (Exception)
                    {
                        ;
                    }
                }
                return View(models);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditAll(List<int> userId)
        {
            List<AppUser> appUser = _userManager.Users.ToList();
            foreach (var item in appUser)
            {
                try
                {
                    await _userManager.RemoveFromRoleAsync(item, "user");
                }
                catch (Exception)
                {

                }

            }

            for (int i = 0; i < userId.Count; i++)
            {
                try
                {
                    AppUser user = await _userManager.FindByIdAsync(userId[i].ToString());
                    if (user != null)
                    {

                        await _userManager.AddToRoleAsync(user, "user");

                    }

                }
                catch (Exception)
                {
                    return RedirectToAction("UserList");
                }


            }
            return RedirectToAction("UserList");



        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

    }
}