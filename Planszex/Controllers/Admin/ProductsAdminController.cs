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
    public class ProductsAdminController : Controller
    {
        private readonly StoreDbContext _context;

        public ProductsAdminController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: ProductsAdmin
        public async Task<IActionResult> Index()
        {
            ViewData["Admin"] = "Tak";
            return View(await _context.Product.ToListAsync());
        }

        // GET: ProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Admin"] = "Tak";
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsAdmin/Create
        public IActionResult Create()
        {
            ViewData["Admin"] = "Tak";
            List<SelectListItem> categoryIds = new List<SelectListItem>();
            List<Category> categories = _context.Set<Category>().ToList();
            foreach (var category in categories)
            {
                SelectListItem temp = new SelectListItem() { Text = category.Name, Value = category.CategoryId.ToString() };
                categoryIds.Add(temp);
            }

            List<SelectListItem> descriptionsIds = new List<SelectListItem>();
            List<Description> descriptions = _context.Set<Description>().ToList();
            foreach (var description in descriptions)
            {
                SelectListItem temp = new SelectListItem() { Text = description.DescriptionId.ToString(), Value = description.DescriptionId.ToString() };
                descriptionsIds.Add(temp);
            }

            categoryIds.First().Selected = true;
            descriptionsIds.First().Selected = true;

            ViewBag.CategoryId = categoryIds;
            ViewBag.DescriptionId = descriptionsIds;
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,DescriptionId,DateOfAdding,ForPromo,Visible,SKU,Name,Price")] Product product)
        {
            ViewData["Admin"] = "Tak";
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            ViewData["Admin"] = "Tak";
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Entry(product).Reference(c => c.Category).Load();

            List<SelectListItem> categoryIds = new List<SelectListItem>();
            List<Category> categories = _context.Set<Category>().ToList();
            foreach(var category in categories)
            {
                SelectListItem temp = new SelectListItem() { Text = category.Name, Value = category.CategoryId.ToString() };
                if(product.CategoryId == category.CategoryId) temp.Selected = true;
                categoryIds.Add(temp);

            }

            List<SelectListItem> descriptionsIds = new List<SelectListItem>();
            List<Description> descriptions = _context.Set<Description>().ToList();
            foreach (var description in descriptions)
            {
                SelectListItem temp = new SelectListItem() { Text = description.DescriptionId.ToString(), Value = description.DescriptionId.ToString() };
                if (product.DescriptionId == description.DescriptionId) temp.Selected = true;
                descriptionsIds.Add(temp);

            }

            ViewBag.CategoryId = categoryIds;
            ViewBag.DescriptionId = descriptionsIds;
            return View(product);
        }

        // POST: ProductsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            ViewData["Admin"] = "Tak";
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Admin"] = "Tak";
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Admin"] = "Tak";
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
