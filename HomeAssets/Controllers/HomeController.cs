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

        public ViewResult ServiceDetail(int id)
        {
            var model = homeServiceRepository.GetById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                return View("HS_NotFound", id);
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateHomeService_vmodel model)
        {
            if (ModelState.IsValid)
            {
                HomeService newHomeService = new HomeService()
                {
                    Location = model.Location,
                    ServiceType = model.ServiceType,
                    Institution = model.Institution,
                    LeasedTo = model.LeasedTo,
                    PaymentCriteria = model.PaymentCriteria,
                    PaymentId = model.PaymentId
                };

                homeServiceRepository.AddHomeService(newHomeService);
                return RedirectToAction("DetailsByServiceType", "Home", new { type = newHomeService.ServiceType });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditHomeService(int id)
        {
            var model = homeServiceRepository.GetById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                return View("HS_NotFound", id);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditHomeService(HomeService model)
        {
            if (ModelState.IsValid)
            {
                homeServiceRepository.UpdateHomeService(model);
                return RedirectToAction("DetailsByServiceType", "Home", new { type = model.ServiceType.ToString() });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteHomeService(int id)
        {
            homeServiceRepository.DeleteHomeService(homeServiceRepository.GetById(id));
            return RedirectToAction("ListAll", "Home");
        }
    }
}