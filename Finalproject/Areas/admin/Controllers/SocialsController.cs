using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finalproject.Data;
using Finalproject.Models;
using Microsoft.AspNetCore.Authorization;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]

    [Authorize]
    public class SocialsController : Controller
    {
        private readonly AppDbContext _context;

        public SocialsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/Socials
        public async Task<IActionResult> Index()
        {
            return View(await _context.Socials.ToListAsync());
        }

        // GET: admin/Socials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socials = await _context.Socials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socials == null)
            {
                return NotFound();
            }

            return View(socials);
        }

        // GET: admin/Socials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Socials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Icon,Link")] Socials socials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socials);
        }

        // GET: admin/Socials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socials = await _context.Socials.FindAsync(id);
            if (socials == null)
            {
                return NotFound();
            }
            return View(socials);
        }

        // POST: admin/Socials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Icon,Link")] Socials socials)
        {
            if (id != socials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialsExists(socials.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(socials);
        }

        // GET: admin/Socials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socials = await _context.Socials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socials == null)
            {
                return NotFound();
            }

            return View(socials);
        }

        // POST: admin/Socials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socials = await _context.Socials.FindAsync(id);
            _context.Socials.Remove(socials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialsExists(int id)
        {
            return _context.Socials.Any(e => e.Id == id);
        }
    }
}
