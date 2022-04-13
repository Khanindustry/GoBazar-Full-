using Finalproject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.ViewComponents
{
    public class VcNavbar : ViewComponent
    {

        private readonly AppDbContext _context;


        public VcNavbar(AppDbContext context)
        {
            _context = context;

        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_context.Categories.Include(m => m.SubCategories).ToList());

        }
    }



}

