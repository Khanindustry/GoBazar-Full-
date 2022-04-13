using Finalproject.Data;
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.ViewComponents
{
    public class VcLayout : ViewComponent
    {
        private readonly AppDbContext _context;


        public VcLayout(AppDbContext context)
        {
            _context = context;

        }
        public IViewComponentResult Invoke()
        {
            VmContact model = new VmContact()
            {
                Socials = _context.Socials.ToList(),
                Setting = _context.Settings.FirstOrDefault()

            };

            return View(model);
        }
    }
}
