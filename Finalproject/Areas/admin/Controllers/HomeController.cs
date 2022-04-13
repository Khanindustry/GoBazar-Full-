using Finalproject.Data;
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchData, int? id)
        {
            VmHome model = new VmHome()
            {
                Products = _context.Products.Where(p => (searchData != null ? p.Brand.Contains(searchData) : true)).ToList(),
                Advertisings = _context.Advertisings.ToList()
            };
            return View(model);
        }


    }
}
