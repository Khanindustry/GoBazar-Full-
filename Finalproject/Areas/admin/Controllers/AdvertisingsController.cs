using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finalproject.Data;
using Finalproject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class AdvertisingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdvertisingsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: admin/Advertisings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advertisings.ToListAsync());
        }

        // GET: admin/Advertisings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertising = await _context.Advertisings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertising == null)
            {
                return NotFound();
            }

            return View(advertising);
        }

        // GET: admin/Advertisings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Advertisings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Advertising advertising)
        {
            if (ModelState.IsValid)
            {
                if (advertising.ImageFile != null)
                {
                    if (advertising.ImageFile.ContentType == "image/jpeg" || advertising.ImageFile.ContentType == "image/png")
                    {
                        if (advertising.ImageFile.Length <= 3000000)
                        {
                            string FileName = Guid.NewGuid() + "-" + advertising.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsAdvertising", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                advertising.ImageFile.CopyTo(stream);
                            }
                            advertising.Image = FileName;
                            _context.Advertisings.Add(advertising);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(advertising);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(advertising);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(advertising);

                }


            }
            return View(advertising);
        }
    

        // GET: admin/Advertisings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertising = await _context.Advertisings.FindAsync(id);
            if (advertising == null)
            {
                return NotFound();
            }
            return View(advertising);
        }

        // POST: admin/Advertisings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Advertising advertising)
        {
            if (ModelState.IsValid)
            {
                if (advertising.ImageFile != null)
                {
                    if (advertising.ImageFile.ContentType == "image/jpeg" || advertising.ImageFile.ContentType == "image/png")
                    {
                        if (advertising.ImageFile.Length <= 3000000)
                        {
                            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsAdvertising", advertising.Image);
                            if (System.IO.File.Exists(olddata))
                            {
                                System.IO.File.Delete(olddata);
                            }
                            string FileName = Guid.NewGuid() + "-" + advertising.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsAdvertising", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                advertising.ImageFile.CopyTo(stream);
                            }
                            advertising.Image = FileName;
                            _context.Advertisings.Update(advertising);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(advertising);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(advertising);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(advertising);

                }


            }
            return View(advertising);
        }

        // GET: admin/Advertisings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertising = await _context.Advertisings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertising == null)
            {
                return NotFound();
            }

            return View(advertising);
        }

        // POST: admin/Advertisings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertising = await _context.Advertisings.FindAsync(id);
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsAdvertising", advertising.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }
            _context.Advertisings.Remove(advertising);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisingExists(int id)
        {
            return _context.Advertisings.Any(e => e.Id == id);
        }
    }
}
