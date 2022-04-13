using Finalproject.Data;
using Finalproject.Models;
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Controllers
{
    public class ContactController : Controller
    {
    
            private readonly AppDbContext _context;

            public ContactController(AppDbContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
            VmContact model = new VmContact()
            {
                Setting = _context.Settings.FirstOrDefault(),

            };

            return View(model);
          
            }

            [HttpPost]
            public IActionResult Message(VmContact model)
            {
                if (ModelState.IsValid)
                {
                    model.Contacts.CreatedDate = DateTime.Now;
                    _context.Contacts.Add(model.Contacts);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("Success", "Your message has been sent successfully!");
                    return RedirectToAction("index");
                }

                HttpContext.Session.SetString("Error", "Model is not valid");
                return RedirectToAction("index");
            }
        }
    
}
