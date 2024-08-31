using DataAcess.Repository;
using DataAcess.Repository.IRepository;
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
        public ClientInformationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                _unitOfWork.ClientInformation.Add(obj.clientInformation);            
                _unitOfWork.Save();
                TempData["success"] = "informations envoyées avec succès !";
                return RedirectToAction("Index");
            }
            else return View(obj);

        }
    }
}
