using Finalproject.Data;
using Finalproject.Models;
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;

        }


        public IActionResult Index(int page = 1, double itemCount = 4)
        {
            VmHome model = new VmHome()
            {
                Blog = _context.Blogs.OrderByDescending(m => m.Id).ToList(),
                Products = _context.Products.ToList(),
                 Advertisings = _context.Advertisings.ToList()
            };

            List<Blog> blogs = _context.Blogs.OrderByDescending(m => m.Id).ToList();
            model.PageCount = (int)Math.Ceiling(blogs.Count / itemCount);
            model.Blog = blogs.Skip((page - 1) * (int)itemCount).Take((int)itemCount).ToList();
            model.Page = page;
            model.ItemCount = itemCount;

            return View(model);
        }
        public IActionResult Blogdetails(int? id)
        {

            return View(_context.Blogs.Find(id));

        }

    }
}
