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
    public class BlogManagementController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly IUnitOfWork _unitOfWork;
        public BlogManagementController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            Blog blog=new Blog();
            if (Id == null || Id == 0)
            {
                return View(blog);
            }
            else
            {
                blog = _unitOfWork.Blog.GetFirstOrDefault(u => u.Id == Id);
                return View(blog);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Blog obj, IFormFile? file, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, "images", "blogs");
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
                    obj.ImgUrl = "/images/blogs/" + FileName + extension;
                }
                if (obj.Id == 0)
                {
                    _unitOfWork.Blog.Add(obj);
                    TempData["success"] = "Blog created successfully";
                }
                else
                {
                    _unitOfWork.Blog.Update(obj);
                    TempData["success"] = "Blog updated successfully";                          
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
            IEnumerable<Blog> List = _unitOfWork.Blog.GetAll();
            return Json(new { data = List });
        }
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _unitOfWork.Blog.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error when deleting" });
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, Obj.ImgUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Blog.Remove(Obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting successfully" });

        }
        #endregion
    }
}
