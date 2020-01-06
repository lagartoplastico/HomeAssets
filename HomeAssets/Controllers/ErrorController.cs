using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssets.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult StatusCodeHandler(int statuscode)
        {
            switch (statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Lo sentimos, la pagina no pudo ser encontrada";
                    break;

                default:
                    break;
            }

            return View("NotFound", statuscode);
        }

        [Route("Error"), AllowAnonymous]
        public IActionResult Error()
        {
            var model = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(model);
        }
    }
}