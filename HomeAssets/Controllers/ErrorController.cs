using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeAssets.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statuscode}"), AllowAnonymous]
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

            return View("NotFound", $"StatusCode={statuscode}");
        }

        [Route("Error"), AllowAnonymous]
        public IActionResult Error()
        {
            var currentException = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"\n{currentException.Path} ---> {currentException.Error.Message}" +
                $"\n*************************************************************************" +
                $"\n{currentException.Error.StackTrace}" +
                $"\n*************************************************************************");

            return View();
        }
    }
}