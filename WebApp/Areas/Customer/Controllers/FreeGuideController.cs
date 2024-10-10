using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using Utility;
using WebApp.Models;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FreeGuideController : Controller
    {
        private readonly ILogger<FreeGuideController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;


        public FreeGuideController(IUnitOfWork unitOfWork, ILogger<FreeGuideController> logger,  IEmailSender emailSender)
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
        public async Task<IActionResult> Index(FreeGuideEmails freeGuideEmails)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FreeGuideEmails.Add(freeGuideEmails);
                _unitOfWork.Save();

                // Sending notification to your professional email
                await _emailSender.SendEmailAsync("contact@ca-web-solutions.net", "New client", "New client has been added from the free guide");

                // Sending the guide PDF to the user
                await _emailSender.SendEmailAsync(freeGuideEmails.Email,
                    "Votre guide gratuit est prêt à être téléchargé !",
                    @"<!DOCTYPE html>
                    <html lang='fr'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Votre Guide Gratuit</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                        <p>Bonjour,</p>

                        <p>Merci d'avoir demandé le guide ultime pour mettre votre entreprise en ligne.</p>
                        <p>Nous vous souhaitons une excellente lecture!</p>
        
                        <p><strong>Chafi Abderrahmane</strong><br>
                        <em>CA Web Solutions</em></p>
        
                        <p style='font-size: 0.9em;'>
                            <strong>PS: </strong>Si vous souhaitez recevoir une analyse gratuite de votre projet, <a href='https://ca-web-solutions.net/Customer/ClientInformations'>veuillez remplir le formulaire</a>.
                        </p>
                    </body>
                    </html>");


                return RedirectToAction("ThanksPage1");
            }
            else
            {
                return View(freeGuideEmails);
            }
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
            List<Blog> blogs = _unitOfWork.Blog.GetAll().Take(3).ToList();
            return View(blogs);
        }
    }
}