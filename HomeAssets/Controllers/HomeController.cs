using HomeAssets.Models;
using HomeAssets.Security;
using HomeAssets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HomeAssets.Controllers
{
    [Authorize(Policy = "ServiceViewers")]
    public class HomeController : Controller
    {
        private readonly IHomeServiceRepo homeServiceRepository;
        private readonly IDataProtector protector;

        public HomeController(IHomeServiceRepo homeServiceRepository,
                              IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.homeServiceRepository = homeServiceRepository;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.HomeAssetIdRouteValue);
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult DetailsByServiceType(string type)
        {
            var model = homeServiceRepository.GetByServiceType(type).Select(s =>
                                {
                                    s.EncryptedId = protector.Protect(s.Id.ToString());
                                    return s;
                                });

            return View(model);
        }

        public ViewResult DetailsByMember(string member)
        {
            var model = homeServiceRepository.GetByMember(member).Select(s =>
                                {
                                    s.EncryptedId = protector.Protect(s.Id.ToString());
                                    return s;
                                });

            return View(model);
        }

        public ViewResult DetailsByLocation(string location)
        {
            var model = homeServiceRepository.GetByLocation(location).Select(s =>
                                {
                                    s.EncryptedId = protector.Protect(s.Id.ToString());
                                    return s;
                                });

            return View(model);
        }

        public ViewResult ListAll()
        {
            var model = homeServiceRepository.GetAllHomeServices().Select(s =>
                                {
                                    s.EncryptedId = protector.Protect(s.Id.ToString());
                                    return s;
                                });

            return View(model);
        }

        public ViewResult ServiceDetail(string id)
        {
            int decryptedId = Convert.ToInt32(protector.Unprotect(id));
            var model = homeServiceRepository.GetById(decryptedId);

            if (model == null)
            {
                Response.StatusCode = 404;
                return View("HS_NotFound", id);
            }
            return View(model);
        }

        [HttpGet, Authorize(Policy = "ServiceManagers")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ServiceManagers")]
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

        [HttpGet, Authorize(Policy = "ServiceManagers")]
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

        [HttpPost, Authorize(Policy = "ServiceManagers")]
        public IActionResult EditHomeService(HomeService model)
        {
            if (ModelState.IsValid)
            {
                homeServiceRepository.UpdateHomeService(model);
                return RedirectToAction("DetailsByServiceType", "Home", new { type = model.ServiceType.ToString() });
            }
            return View(model);
        }

        [HttpPost, Authorize(Policy = "ServiceManagers")]
        public IActionResult DeleteHomeService(int id)
        {
            homeServiceRepository.DeleteHomeService(homeServiceRepository.GetById(id));
            return RedirectToAction("ListAll", "Home");
        }
    }
}