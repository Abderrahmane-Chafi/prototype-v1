using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class FreeGuideEmailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FreeGuideEmailsController(IUnitOfWork unitOfWork)
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
            IEnumerable<FreeGuideEmails> List = _unitOfWork.FreeGuideEmails.GetAll();
            return Json(new { data = List });
        }
        #endregion
    }
}
