using HomeAssets.Models;
using HomeAssets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssets.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeServiceRepo homeServiceRepository;

        public HomeController(IHomeServiceRepo homeServiceRepository)
        {
            this.homeServiceRepository = homeServiceRepository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult DetailsByServiceType(string type)
        {
            var model = homeServiceRepository.GetByServiceType(type);

            return View(model);
        }

        public ViewResult DetailsByMember(string member)
        {
            var model = homeServiceRepository.GetByMember(member);

            return View(model);
        }

        public ViewResult DetailsByLocation(string location)
        {
            var model = homeServiceRepository.GetByLocation(location);

            return View(model);
        }

        public ViewResult ListAll()
        {
            var model = homeServiceRepository.GetAllHomeServices();

            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateHomeService_vm model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("index");
            }
            return View(model);
        }
    }
}