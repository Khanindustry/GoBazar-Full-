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
    public class HomeImage1Controller : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeImage1Controller(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: admin/HomeImage1
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeImage1s.ToListAsync());
        }

        // GET: admin/HomeImage1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeImage1 = await _context.HomeImage1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeImage1 == null)
            {
                return NotFound();
            }

            return View(homeImage1);
        }

        // GET: admin/HomeImage1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/HomeImage1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( HomeImage1 homeImage1)
        {
            if (ModelState.IsValid)
            {
                if (homeImage1.ImageFile != null)
                {
                    if (homeImage1.ImageFile.ContentType == "image/jpeg" || homeImage1.ImageFile.ContentType == "image/png")
                    {
                        if (homeImage1.ImageFile.Length <= 3000000)
                        {
                            string FileName = Guid.NewGuid() + "-" + homeImage1.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsHomeImgae", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                homeImage1.ImageFile.CopyTo(stream);
                            }
                            homeImage1.Image = FileName;
                            _context.HomeImage1s.Add(homeImage1);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(homeImage1);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(homeImage1);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(homeImage1);

                }


            }
            return View(homeImage1);
        }

        // GET: admin/HomeImage1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeImage1 = await _context.HomeImage1s.FindAsync(id);
            if (homeImage1 == null)
            {
                return NotFound();
            }
            return View(homeImage1);
        }

        // POST: admin/HomeImage1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  HomeImage1 homeImage1)
        {
            if (ModelState.IsValid)
            {
                if (homeImage1.ImageFile != null)
                {
                    if (homeImage1.ImageFile.ContentType == "image/jpeg" || homeImage1.ImageFile.ContentType == "image/png")
                    {
                        if (homeImage1.ImageFile.Length <= 3000000)
                        {
                            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsHomeImgae", homeImage1.Image);
                            if (System.IO.File.Exists(olddata))
                            {
                                System.IO.File.Delete(olddata);
                            }
                            string FileName = Guid.NewGuid() + "-" + homeImage1.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsHomeImgae", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                homeImage1.ImageFile.CopyTo(stream);
                            }
                            homeImage1.Image = FileName;
                            _context.HomeImage1s.Update(homeImage1);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(homeImage1);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(homeImage1);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(homeImage1);

                }


            }
            return View(homeImage1);
        }

        // GET: admin/HomeImage1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeImage1 = await _context.HomeImage1s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeImage1 == null)
            {
                return NotFound();
            }

            return View(homeImage1);
        }

        // POST: admin/HomeImage1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeImage1 = await _context.HomeImage1s.FindAsync(id);
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsHomeImgae", homeImage1.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }

            _context.HomeImage1s.Remove(homeImage1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeImage1Exists(int id)
        {
            return _context.HomeImage1s.Any(e => e.Id == id);
        }
    }
}
