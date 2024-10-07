using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FreeGuideController : Controller
    {
        private readonly ILogger<FreeGuideController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;


        public FreeGuideController(IUnitOfWork unitOfWork, ILogger<FreeGuideController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            FreeGuideEmails freeGuideEmails = new();
            return View(freeGuideEmails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FreeGuideEmails freeGuideEmails)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FreeGuideEmails.Add(freeGuideEmails);
                _unitOfWork.Save();
                _emailSender.SendEmailAsync("contact@ca-web-solutions.net", "New client", "New client has been added from the free guide");
                return RedirectToAction("ThanksPage1");
            }
            else return View(freeGuideEmails);
        }
        public IActionResult ThanksPage1()
        {
            var means = new List<string>() { "Grâce à une recherche Google.",
                "Un ami m'a recommandé vos services.",
                "J'ai vu votre publicité sur les réseaux sociaux.",
                "J'ai reçu un e-mail à propos de vos services.",
                "J'ai découvert votre entreprise par hasard en surfant sur Internet.",
            };
            ClientInformationsVM clientInformationsVM = new()
            {
                clientInformation = new(),
                meansList = means.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
               ,
            };
            return View(clientInformationsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanksPage1(ClientInformationsVM obj)
        {
            if (ModelState.IsValid)
            {
                obj.clientInformation.type = "Free guide";
                _unitOfWork.ClientInformation.Add(obj.clientInformation);
                _unitOfWork.Save();
                //TempData["success"] = "informations envoyées avec succès !";
                _emailSender.SendEmailAsync("contact@ca-web-solutions.net", "New client has been added", obj.clientInformation.Name + " from " + obj.clientInformation.CompanyName + " has been added to the database");
                return RedirectToAction("ThanksPage2");
            }
            else return View(obj);

        }
        public IActionResult ThanksPage2()
        {
            return View();
        }
    }
}