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
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class SubCategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubCategoriesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: admin/SubCategories
        public async Task<IActionResult> Index(int page = 1, double itemCount = 10)
        {

            VmHome model = new VmHome()
            {

                Products = _context.Products.ToList(),

            };

            List<SubCategory> subCategories = _context.SubCategories.OrderByDescending(m => m.Id).ToList();
            model.PageCount = (int)Math.Ceiling(subCategories.Count / itemCount);
            model.SubCategories = subCategories.Skip((page - 1) * (int)itemCount).Take((int)itemCount).ToList();
            model.Page = page;
            model.ItemCount = itemCount;

            return View(model);

            var appDbContext = _context.SubCategories.OrderByDescending(m => m.Id).Include(s => s.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: admin/SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: admin/SubCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: admin/SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                if (subCategory.ImageFile != null)
                {
                    if (subCategory.ImageFile.ContentType == "image/jpeg" || subCategory.ImageFile.ContentType == "image/png")
                    {
                        if (subCategory.ImageFile.Length <= 3000000)
                        {
                            string FileName = Guid.NewGuid() + "-" + subCategory.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsSubcategory", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                subCategory.ImageFile.CopyTo(stream);
                            }
                            subCategory.Image = FileName;
                            _context.SubCategories.Add(subCategory);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(subCategory);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(subCategory);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(subCategory);

                }


            }
            return View(subCategory);
        }

        // GET: admin/SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", subCategory.CategoryId);
            return View(subCategory);
        }

        // POST: admin/SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                if (subCategory.ImageFile != null)
                {
                    if (subCategory.ImageFile.ContentType == "image/jpeg" || subCategory.ImageFile.ContentType == "image/png")
                    {
                        if (subCategory.ImageFile.Length <= 3000000)
                        {
                            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsSubcategory", subCategory.Image);
                            if (System.IO.File.Exists(olddata))
                            {
                                System.IO.File.Delete(olddata);
                            }
                            string FileName = Guid.NewGuid() + "-" + subCategory.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsSubcategory", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                subCategory.ImageFile.CopyTo(stream);
                            }
                            subCategory.Image = FileName;
                            _context.SubCategories.Update(subCategory);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(subCategory);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(subCategory);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(subCategory);

                }


            }
            return View(subCategory);
        }

        // GET: admin/SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: admin/SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsSubcategory", subCategory.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.Id == id);
        }
    }
}
