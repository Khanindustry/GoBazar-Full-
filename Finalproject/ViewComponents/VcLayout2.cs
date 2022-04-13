using Finalproject.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.ViewComponents
{
    public class VcLayout2 : ViewComponent
    {
        private readonly AppDbContext _context;


        public VcLayout2(AppDbContext context)
        {
            _context = context;

        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Settings.FirstOrDefault());

        }
    }
}
