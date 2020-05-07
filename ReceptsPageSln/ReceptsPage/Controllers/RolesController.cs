using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PagedList.Core;
using ReceptsPage.IdentityViewModels;
using ReceptsPage.ModelIdentity;
using ReceptsPage.Models;

namespace ReceptsPage.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<AppRole> _roleManager;
        UserManager<AppUser> _userManager;

        public IConfiguration _configuration { get; }

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public IActionResult Roles() => View(_roleManager.Roles.ToList());
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
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
            return RedirectToAction("Index", "Users");
        }


        [HttpGet]
        public async Task<IActionResult> EditAll(int? page)
        {
            if (_userManager.Users != null)
            {
                int allUserRoleUser = 0;
                UserRoleAllViewModel model = new UserRoleAllViewModel();

                model.AllUsers = _userManager.Users.ToList();
                model.AllUsersPage = _userManager.Users.ToPagedList(page ?? 1, 5);

                foreach (var user in _userManager.Users.ToList())
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Contains("user"))
                    {
                        allUserRoleUser++;
                    }

                    model.userRoles.Add(new UserRolesOnlyViewModel() { UserRoles = userRoles, Id = user.Id, Name = user.Email });

                }
                model.AllUsersRoleUser = allUserRoleUser;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditAll(List<int> userId, List<int> userId1)
        {
            var userRemoveIDs = userId1.Except(userId).ToList();
            for (int i = 0; i < userRemoveIDs.Count; i++)
            {
                try
                {
                    AppUser user = await _userManager.FindByIdAsync(userRemoveIDs[i].ToString());
                    if (user != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user, "user");
                    }
                }
                catch (Exception) { }
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
                    return RedirectToAction("Roles");
                }
            }
            return RedirectToAction("EditAll");

        }

        [HttpGet]
        public async Task<IActionResult> AdminEdit()
        {
            // получаем пользователя
            List<AppUser> adminUsers = new List<AppUser>();


            List<int> UserInt = new List<int>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"select [UserId] from [dbo].[AspNetUserRoles] where roleId = 1", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            UserInt.Add((((int)reader["UserId"])));
                        }
                    }
                }
                connection.Close();
            }

            for (int i = 0; i < UserInt.Count; i++)
            {
                try
                {
                    adminUsers.Add(_userManager.Users.First(u => u.Id == UserInt[i]));
                }
                catch (Exception)
                {
                    ;
                }
            }

            var allUser = _userManager.Users.ToList();
            AdminRoleViewModel model = new AdminRoleViewModel();
            model.AdminUsers = adminUsers;
            foreach (var user in _userManager.Users.ToList())
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                model.userRoles.Add(new UserRolesOnlyViewModel() { UserRoles = userRoles, Id = user.Id, Name = user.Email });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminEdit(List<int> userId)
        {
            List<AppUser> appUser = _userManager.Users.ToList();
            foreach (var item in appUser)
            {
                try
                {
                    await _userManager.RemoveFromRoleAsync(item, "admin");
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
                        await _userManager.AddToRoleAsync(user, "admin");
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Users");
                }

            }
            return RedirectToAction("Index", "Users");


        }
        public async Task<IActionResult> Edit(string userId)
        {
       
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                
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
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
          
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
               
                var userRoles = await _userManager.GetRolesAsync(user);
              
                var allRoles = _roleManager.Roles.ToList();
             
                var addedRoles = roles.Except(userRoles);
             
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index","Users");
            }

            return View("NotFound");
        }

    }
}