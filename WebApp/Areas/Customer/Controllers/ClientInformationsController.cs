using DataAcess.Repository;
using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class ClientInformationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        public ClientInformationsController(IUnitOfWork unitOfWork,IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender; 
        }
        public IActionResult Index()
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
        public IActionResult Index(ClientInformationsVM obj)
        {              
            if (ModelState.IsValid) {
                obj.clientInformation.type = "Normal";
                _unitOfWork.ClientInformation.Add(obj.clientInformation);                
                _unitOfWork.Save();
                TempData["success"] = "informations envoyées avec succès !";
                _emailSender.SendEmailAsync("contact@ca-web-solutions.net", "New client has been added", obj.clientInformation.Name + " from " + obj.clientInformation.CompanyName + " has been added to the database");
                return RedirectToAction("Index");
            }
            else return View(obj);

        }
    }
}
