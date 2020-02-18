using HomeAssets.Models;
using HomeAssets.Models.ExtendedIdentity;
using HomeAssets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeAssets.Controllers
{
    [Authorize(Policy = "AccountViewers")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<App_IdentityUser> userManager;
        private readonly IAuthorizedEmailRepo authorizedEmailRepository;

        public AdministrationController(UserManager<App_IdentityUser> userManager, IAuthorizedEmailRepo authorizedEmailRepository)
        {
            this.userManager = userManager;
            this.authorizedEmailRepository = authorizedEmailRepository;
        }

        public IActionResult ListUsers()
        {
            var model = userManager.Users.OrderBy(u => u.UserName);

            return View(model);
        }

        [HttpGet, Authorize(Policy = "AccountManagers")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("NotFound", $"user id=[{id}] not found");
            }

            var userClaims = await userManager.GetClaimsAsync(user);

            var model = new EditAccount_vmodel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Gender = user.Gender,
                Claims = userClaims.Select(c => c.Value).ToList()
            };

            return View(model);
        }

        [HttpPost, Authorize(Policy = "AccountManagers")]
        public async Task<IActionResult> EditUser(EditAccount_vmodel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return View("NotFound", $"user id=[{model.Id}] not found");
                }
                user.UserName = model.Username;
                user.Email = model.Email;
                user.Gender = model.Gender;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost, Authorize(Policy = "AccountManagers")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFound", $"user id=[{id}] not found");
            }
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("ListUsers");
        }

        [HttpGet, Authorize(Policy = "AccountManagers")]
        public async Task<IActionResult> ManageUserClaims(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("NotFound", $"user id=[{id}] not found");
            }

            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaims_vmodel()
            {
                UserId = id,
                UserName = user.UserName
            };

            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };

                if (existingUserClaims.Any(c => (c.Type == claim.Type && c.Value == claim.Value)))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }

            return View(model);
        }

        [HttpPost, Authorize(Policy = "AccountManagers")]
        public async Task<IActionResult> ManageUserClaims(UserClaims_vmodel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return View("NotFound", $"user id=[{model.UserId}] not found");
            }

            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "No se pudo remover las claims de usuario");
                return View(model);
            }

            result = await userManager.AddClaimsAsync(user,
                        model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimValue)));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "No se pudo agregar claims a el usuario");
                return View(model);
            }

            return RedirectToAction("EditUser", new { id = model.UserId });
        }

        [HttpGet, Authorize(Policy = "AccountManagers")]
        public IActionResult AuthorizedEmails()
        {
            var model = new AuthorizedEmails_vmodel();
            if (authorizedEmailRepository.GetAllAuthorizedEmails().Any())
            {
                foreach (var authorizeEmail in authorizedEmailRepository.GetAllAuthorizedEmails())
                {
                    model.AlreadyAuthorizedEmails.Add(authorizeEmail.EmailAddress);
                }
            }
            return View(model);
        }

        [HttpPost, Authorize(Policy = "AccountManagers")]
        public IActionResult DeleteAuthorizedEmails(string email)
        {
            if (email!=null)
            {
                authorizedEmailRepository.RemoveAuthorizedEmail(email);
            }
            return RedirectToAction("AuthorizedEmails");
        }

        [HttpGet, Authorize(Policy = "AccountManagers")]
        public IActionResult AuthorizeAnEmail()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "AccountManagers")]
        public IActionResult AuthorizeAnEmail(AuthorizeAnEmail_vmodel model)
        {
            if (ModelState.IsValid)
            {
                if (authorizedEmailRepository.GetByEmail(model.Email) == null)
                {
                    authorizedEmailRepository.AddAuthorizedEmail(model.Email);
                }
                return RedirectToAction("AuthorizedEmails");
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsEmailAlreadyAuthorized(string email)
        {
            if (authorizedEmailRepository.GetByEmail(email) == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"El correo '{email}' ya está autorizado para registrarse");
            }
        }
    }
}