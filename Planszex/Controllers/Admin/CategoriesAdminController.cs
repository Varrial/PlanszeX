using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planszex.Models;
using Repository;

namespace Planszex.Controllers.Admin
{
    public class CategoriesAdminController : Controller
    {
        private readonly StoreDbContext _context;

        public CategoriesAdminController(StoreDbContext context)
        {
            ViewData["Admin"] = "Tak";
            _context = context;
        }

        // GET: CategoriesAdmin
        public async Task<IActionResult> Index()
        {
            ViewData["Admin"] = "Tak";
            return View(await _context.Category.ToListAsync());
        }

        // GET: CategoriesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Admin"] = "Tak";
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: CategoriesAdmin/Create
        public IActionResult Create()
        {
            ViewData["Admin"] = "Tak";
            return View();
        }

        // POST: CategoriesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,Name")] Category category)
        {
            ViewData["Admin"] = "Tak";
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoriesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Admin"] = "Tak";
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoriesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,Name")] Category category)
        {
            ViewData["Admin"] = "Tak";
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: CategoriesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Admin"] = "Tak";
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoriesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Admin"] = "Tak";
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            ViewData["Admin"] = "Tak";
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
