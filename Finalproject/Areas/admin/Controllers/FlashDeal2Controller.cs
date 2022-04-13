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
    public class FlashDeal2Controller : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FlashDeal2Controller(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: admin/FlashDeal2
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlashDeal2s.ToListAsync());
        }

        // GET: admin/FlashDeal2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flashDeal2 = await _context.FlashDeal2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flashDeal2 == null)
            {
                return NotFound();
            }

            return View(flashDeal2);
        }

        // GET: admin/FlashDeal2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/FlashDeal2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( FlashDeal2 flashDeal2)
        {
            if (ModelState.IsValid)
            {
                if (flashDeal2.ImageFile != null)
                {
                    if (flashDeal2.ImageFile.ContentType == "image/jpeg" || flashDeal2.ImageFile.ContentType == "image/png")
                    {
                        if (flashDeal2.ImageFile.Length <= 3000000)
                        {
                            string FileName = Guid.NewGuid() + "-" + flashDeal2.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsFlas", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                flashDeal2.ImageFile.CopyTo(stream);
                            }
                            flashDeal2.Image = FileName;
                            _context.FlashDeal2s.Add(flashDeal2);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(flashDeal2);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(flashDeal2);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(flashDeal2);

                }


            }
            return View(flashDeal2);
        }

        // GET: admin/FlashDeal2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flashDeal2 = await _context.FlashDeal2s.FindAsync(id);
            if (flashDeal2 == null)
            {
                return NotFound();
            }
            return View(flashDeal2);
        }

        // POST: admin/FlashDeal2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FlashDeal2 flashDeal2)
        {
          
                if (ModelState.IsValid)
                {
                    if (flashDeal2.ImageFile != null)
                    {
                        if (flashDeal2.ImageFile.ContentType == "image/jpeg" || flashDeal2.ImageFile.ContentType == "image/png")
                        {
                            if (flashDeal2.ImageFile.Length <= 3000000)
                            {
                                string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsFlas", flashDeal2.Image);
                                if (System.IO.File.Exists(olddata))
                                {
                                    System.IO.File.Delete(olddata);
                                }
                                string FileName = Guid.NewGuid() + "-" + flashDeal2.ImageFile.FileName;
                                string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsFlas", FileName);
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    flashDeal2.ImageFile.CopyTo(stream);
                                }
                                flashDeal2.Image = FileName;
                                _context.FlashDeal2s.Update(flashDeal2);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));

                            }
                            else
                            {
                                ModelState.AddModelError("", "you can choose only 3 mb image file");
                                return View(flashDeal2);
                            }


                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only image file");
                            return View(flashDeal2);

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", " choose image file");
                        return View(flashDeal2);

                    }

                
            }
            return View(flashDeal2);
        }

        // GET: admin/FlashDeal2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flashDeal2 = await _context.FlashDeal2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flashDeal2 == null)
            {
                return NotFound();
            }

            return View(flashDeal2);
        }

        // POST: admin/FlashDeal2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flashDeal2 = await _context.FlashDeal2s.FindAsync(id);
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsFlas", flashDeal2.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }
            _context.FlashDeal2s.Remove(flashDeal2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlashDeal2Exists(int id)
        {
            return _context.FlashDeal2s.Any(e => e.Id == id);
        }
    }
}
