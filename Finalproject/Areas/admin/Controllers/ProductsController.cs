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
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: admin/Products
        public async Task<IActionResult> Index(int page = 1, double itemCount = 10)
        {
            VmHome model = new VmHome()
            {
                
                Products = _context.Products.ToList(),
             
            };

            List<Product> products = _context.Products.OrderByDescending(m => m.Id).ToList();
            model.PageCount = (int)Math.Ceiling(products.Count / itemCount);
            model.Products = products.Skip((page - 1) * (int)itemCount).Take((int)itemCount).ToList();
            model.Page = page;
            model.ItemCount = itemCount;

            return View(model);




            var appDbContext = _context.Products.OrderByDescending(m=>m.Id).Include(p => p.Category).Include(p => p.SubCategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoryList = _context.Categories.ToList();
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "Id", "Id");

            return View();
        }
        public JsonResult GetSubCategory(int categoryId)
        {
            var data = _context.SubCategories.Where(m => m.CategoryId == categoryId).ToList();
            return Json(data);
        }


        // POST: admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    if (product.ImageFile.ContentType == "image/jpeg" || product.ImageFile.ContentType == "image/png")
                    {
                        if (product.ImageFile.Length <= 3000000)
                        {
                            string FileName = Guid.NewGuid() + "-" + product.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsProducts", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                product.ImageFile.CopyTo(stream);
                            }
                            product.Image = FileName;
                            _context.Products.Add(product);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(product);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(product);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(product);

                }

               
            }
            ViewBag.CategoryList = _context.Categories.ToList();
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "Id", "Name", product.SubCategoryId);
            return View(product);
        }

        // GET: admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryList = _context.Categories.ToList();
            return View(product);
        }

        // POST: admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    if (product.ImageFile.ContentType == "image/jpeg" || product.ImageFile.ContentType == "image/png")
                    {
                        if (product.ImageFile.Length <= 3000000)
                        {
                            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsProducts", product.Image);
                            if (System.IO.File.Exists(olddata))
                            {
                                System.IO.File.Delete(olddata);
                            }
                            string FileName = Guid.NewGuid() + "-" + product.ImageFile.FileName;
                            string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsProducts", FileName);
                            using (var stream = new FileStream(FilePath, FileMode.Create))
                            {
                                product.ImageFile.CopyTo(stream);
                            }
                            product.Image = FileName;
                            _context.Products.Update(product);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only 3 mb image file");
                            return View(product);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "you can choose only image file");
                        return View(product);

                    }

                }
                else
                {
                    ModelState.AddModelError("", " choose image file");
                    return View(product);

                }


            }


            ViewBag.CategoryList = _context.Categories.ToList();
         
          
            return View(product);
        }

        // GET: admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "UploadsProducts", product.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
