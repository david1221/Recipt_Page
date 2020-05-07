using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceptsPage.IdentityViewModels;
using ReceptsPage.ModelIdentity;

namespace ReceptsPage.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        /// <summary>
        /// Register User GET Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Register()
        {
            Dictionary<string, string> genderDictionary = new Dictionary<string, string>() { { "male", "Արական" }, { "female", "Իգական" } };
            var a = new List<Dictionary<string, string>>();
            a.Add(genderDictionary);
            ViewBag.Gender = a;
            return View();
        }
        /// <summary>
        /// Register User post Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    
                    AppUser user = new AppUser { Email = model.Email, UserName = model.Email, Gender = model.Gender };
                    // добавляем пользователя
                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    var roleResult = await _userManager.AddToRoleAsync(user, "user");
                    if (result.Succeeded && roleResult.Succeeded)
                    {
                        // установка куки
                        await _signInManager.SignInAsync(user, false);
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
                catch (Exception)
                {

                    ViewBag.ErrorTitle = "Գրանցվելը չի ստացվել";
                    ViewBag.ErrorMessege = "Փորձեք կրկին, եթե չի ստացվում գրանցումը ավարտել ,խնդրում ենք կապ հաստատել մեր սպասարկման բաժնի հետ info.pagerecepts@gmail.com Էլեկրոնային հասցեի միջոցով";
                    return View("Error");
                    }

            }
            return View(model);
        }
        /// <summary>
        /// Login User With local GET Method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Artickles");
            }
            else
            {
                LoginViewModel model = new LoginViewModel()
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                };

                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Artickles");
                }
                return View(model);
            }
        }
        /// <summary>
        /// Login User With local Post Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Articles");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Սխալ Մուտքանուն կամ գաղտնաբառ");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Externial Login Post Method
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternialLogin(string provider, string returnUrl)
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { redirecturl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        ///  External Login Callback GetMEthod
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="remoteError"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                        (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Սխալը արտաքին մատակարարից: {remoteError}");

                return View("Login", loginViewModel);
            }

            // Get the login information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Արտաքին մուտքի տեղեկատվությունը բեռնելու սխալ.");

                return View("Login", loginViewModel);
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);


            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new AppUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),

                        };

                        await _userManager.CreateAsync(user);


                    }

                    var roleResult = await _userManager.AddToRoleAsync(user, "user");

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Էլ. Փոստով հայց չի ստացվել: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Խնդրում եմ կապվել receps.page.info@gmail.com";

                return View("Error");
            }
        }

        /// <summary>
        /// Cofirm Email GET Method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Person", "Users");
            else
                return View("Error");
        }

        /// <summary>      
        /// ConfirmEmailRequest Method 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailRequest()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Account",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"Անցեք հետևյալ հղումով հաստատելու համար էլեկտրոնային հասցեն: <a href='{callbackUrl}'>link</a>");

                return RedirectToAction("Person", "Users");
            }

            return RedirectToAction("Index", "Articles");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null )
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Վերականգնել գաղտնաբառը",
                    $"Ձեր Գաղտնաբառի վերականգնման համար անցեք հետևյալ  հղումով: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        /// <summary>
        /// Logout Post Method
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Articles");
        }
    }
}