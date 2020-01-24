using HomeAssets.Models.ExtendedIdentity;
using HomeAssets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAssets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<App_IdentityUser> userManager;
        private readonly SignInManager<App_IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<App_IdentityUser> userManager,
            SignInManager<App_IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
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
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                                      new { userId = user.Id, token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, $">>>EMAIL VALIDATOR>>>\n{confirmationLink}\n-------------------------");

                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);

                    if ((await userManager.GetClaimsAsync(user)).Count() > 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("WithoutClaims", new { emailConfirmed = user.EmailConfirmed });
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
        public async Task<IActionResult> Login(string returnUrl)
        {
            SignIn_vmodel model = new SignIn_vmodel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(SignIn_vmodel model, string returnUrl)
        {
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

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

                if (user != null && (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    if (!user.EmailConfirmed)
                    {
                        ModelState.AddModelError(string.Empty, "Email no confirmado aún");
                        return View(model);
                    }
                    var result = await signInManager.PasswordSignInAsync(user, model.Password,
                                                                         model.PersistentCookies, false);
                    if (result.Succeeded && (await userManager.GetClaimsAsync(user)).Count != 0)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.Succeeded)
                    {
                        return RedirectToAction("WithoutClaims", new { emailConfirmed = user.EmailConfirmed });
                    }
                }
                ModelState.AddModelError(string.Empty, " Intento invalido de inicio de sesión");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnURL)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnURL });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");

            SignIn_vmodel model = new SignIn_vmodel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", model);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");
                return View("Login", model);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            App_IdentityUser user;

            if (email != null)
            {
                user = await userManager.FindByEmailAsync(email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email no confirmado aún");
                    return View("Login", model);
                }
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                                                                            isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                user = await userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
                if ((await userManager.GetClaimsAsync(user)).Count() > 0)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("WithoutClaims", new { emailConfirmed = user.EmailConfirmed });
            }
            else
            {
                user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    string username = info.Principal.FindFirstValue(ClaimTypes.Name);
                    if (username.Contains(" "))
                    {
                        username = username.Split(' ')[0].ToLower();
                    }
                    else
                    {
                        username = username.ToLower();
                    }
                    user = new App_IdentityUser()
                    {
                        UserName = username,
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };

                    var result = await userManager.CreateAsync(user);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, $"{error.Description}");
                        }
                        return View("Login", model);
                    }

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                                      new { userId = user.Id, token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, $">>>EMAIL VALIDATOR>>>\n{confirmationLink}\n-------------------------"); 
                }

                await userManager.AddLoginAsync(user, info);
                await signInManager.SignInAsync(user, isPersistent: false);

                if ((await userManager.GetClaimsAsync(user)).Count() > 0)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("WithoutClaims", new { emailConfirmed = user.EmailConfirmed });
            }
        }

        [HttpGet]
        public ViewResult WithoutClaims(bool emailConfirmed)
        {
            return View(emailConfirmed);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View("Error");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("NotFound", $"El id de usuario es invalido");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            return View(result.Succeeded);
        }
    }
}