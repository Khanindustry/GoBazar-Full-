using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finalproject.Data;
using Finalproject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class BrandsLogoesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandsLogoesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: admin/BrandsLogoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BrandsLogos.ToListAsync());
        }

        // GET: admin/BrandsLogoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandsLogo = await _context.BrandsLogos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandsLogo == null)
            {
                return NotFound();
            }

            return View(brandsLogo);
        }

        // GET: admin/BrandsLogoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/BrandsLogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandsLogo brandsLogo)
        {
            if (ModelState.IsValid)
            {
                if (brandsLogo.ImageFile != null)
                {
                    if (brandsLogo.ImageFile.ContentType == "image/jpeg" || brandsLogo.ImageFile.ContentType == "image/png")
                    {
                        if (brandsLogo.ImageFile.Length <= 3000000)
                        {
                            string FileName = Guid.NewGuid() + "-" + brandsLogo.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsBrandlogo", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                brandsLogo.ImageFile.CopyTo(stream);
                            }
                            brandsLogo.Image = FileName;
                            _context.BrandsLogos.Add(brandsLogo);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(brandsLogo);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(brandsLogo);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(brandsLogo);

                }


            }
            return View(brandsLogo);
        }

            // GET: admin/BrandsLogoes/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandsLogo = await _context.BrandsLogos.FindAsync(id);
            if (brandsLogo == null)
            {
                return NotFound();
            }
            return View(brandsLogo);
        }

        // POST: admin/BrandsLogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandsLogo brandsLogo)
        {
            if (ModelState.IsValid)
            {
                if (brandsLogo.ImageFile != null)
                {
                    if (brandsLogo.ImageFile.ContentType == "image/jpeg" || brandsLogo.ImageFile.ContentType == "image/png")
                    {
                        if (brandsLogo.ImageFile.Length <= 3000000)
                        {
                            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsBrandlogo", brandsLogo.Image);
                            if (System.IO.File.Exists(olddata))
                            {
                                System.IO.File.Delete(olddata);
                            }
                            string FileName = Guid.NewGuid() + "-" + brandsLogo.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsBrandlogo", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                brandsLogo.ImageFile.CopyTo(stream);
                            }
                            brandsLogo.Image = FileName;
                            _context.BrandsLogos.Update(brandsLogo);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(brandsLogo);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(brandsLogo);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(brandsLogo);

                }


            }
            return View(brandsLogo);
        }

        // GET: admin/BrandsLogoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandsLogo = await _context.BrandsLogos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandsLogo == null)
            {
                return NotFound();
            }

            return View(brandsLogo);
        }

        // POST: admin/BrandsLogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brandsLogo = await _context.BrandsLogos.FindAsync(id);
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsBrandlogo", brandsLogo.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }
            _context.BrandsLogos.Remove(brandsLogo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandsLogoExists(int id)
        {
            return _context.BrandsLogos.Any(e => e.Id == id);
        }
    }
}
