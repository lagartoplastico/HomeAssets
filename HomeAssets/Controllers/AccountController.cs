using HomeAssets.Models.ExtendedIdentity;
using HomeAssets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAssets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<App_IdentityUser> userManager;
        private readonly SignInManager<App_IdentityUser> signInManager;

        public AccountController(UserManager<App_IdentityUser> userManager,
            SignInManager<App_IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(CreateAccount_vmodel model)
        {
            if (ModelState.IsValid)
            {
                var user = new App_IdentityUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Gender = model.Gender
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameInUse(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"El nombre de usuario '{username}' ya está en uso");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"El correo '{email}' ya está en uso");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(SignIn_vmodel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                App_IdentityUser user;

                if (model.UserOrEmail.Contains('@'))
                {
                    user = await userManager.FindByEmailAsync(model.UserOrEmail);
                }
                else
                {
                    user = await userManager.FindByNameAsync(model.UserOrEmail);
                }

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password,
                                                                         model.PersistentCookies, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, " Intento invalido de inicio de sesión");
            }
            return View(model);
        }
    }
}