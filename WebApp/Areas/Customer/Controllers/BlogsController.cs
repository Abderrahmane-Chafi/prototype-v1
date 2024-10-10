using DataAcess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using WebApp.Models;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BlogsController : Controller
    {
        private readonly ILogger<BlogsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BlogsController(IUnitOfWork unitOfWork, ILogger<BlogsController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable <Blog> blogs = _unitOfWork.Blog.GetAll();  
            return View(blogs);
        }
        public IActionResult detail(int id)
        {
            BlogVM blogVM = new()
            {
                blog = _unitOfWork.Blog.GetFirstOrDefault(u => u.Id == id),
                BlogList = _unitOfWork.Blog.GetAll().Take(3)
               
            };
            return View(blogVM);
        }
    }
}