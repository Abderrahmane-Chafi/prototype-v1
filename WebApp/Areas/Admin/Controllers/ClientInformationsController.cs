using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ClientInformationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientInformationsController(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region API CALLS
        //POST
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ClientInformation> List = _unitOfWork.ClientInformation.GetAll();
            return Json(new { data = List });
        }
        #endregion
    }
}
