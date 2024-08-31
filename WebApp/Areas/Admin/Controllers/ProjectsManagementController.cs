using DataAcess.Repository;
using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Models;
using System.Security.Claims;
using Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProjectsManagementController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly IUnitOfWork _unitOfWork;
        public ProjectsManagementController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? Id)
        {
            RealisedProjects realisedProjects=new RealisedProjects();
            if (Id == null || Id == 0)
            {
                return View(realisedProjects);
            }
            else
            {
                realisedProjects = _unitOfWork.RealisedProjects.GetFirstOrDefault(u => u.Id == Id);
                return View(realisedProjects);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RealisedProjects obj, IFormFile? file, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, "images", "projects");
                    //keep same extension
                    var extension = Path.GetExtension(file.FileName);

                    //update the image - Check if there is an image
                    if (obj.ImgUrl != null)
                    {
                        //old image path
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImgUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    //copy the file uploaded inside the product folder
                    using (var fileStreams = new FileStream(Path.Combine(uploads, FileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    //what we will save in the DB
                    obj.ImgUrl = "/images/projects/" + FileName + extension;
                }
                if (obj.Id == 0)
                {
                    _unitOfWork.RealisedProjects.Add(obj);
                    TempData["success"] = "project created successfully";
                }
                else
                {
                    _unitOfWork.RealisedProjects.Update(obj);
                    TempData["success"] = "project updated successfully";                          
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
            

        }
        #region API CALLS
        //POST
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<RealisedProjects> List = _unitOfWork.RealisedProjects.GetAll();
            return Json(new { data = List });
        }
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _unitOfWork.RealisedProjects.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error when deleting" });
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, Obj.ImgUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.RealisedProjects.Remove(Obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting successfully" });

        }
        #endregion
    }
}
