using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planszex.Models;
using Planszex.Services;
using Repository;

namespace Planszex.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly StoreDbContext _context;

        public class IndexViewModel
        {
            public Product product { get; set; }
            public string productLink { get; set; }
            public ProductPrice price { get; set; }
        }
        public CategoriesController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            string iOPstring = SessionService.GetSession<string>(HttpContext.Session, "ItemsOnPage");
            int itemsOnPage = int.Parse(iOPstring);
            List<IndexViewModel> indexViewModels = new List<IndexViewModel>();
            IndexViewModel index;
            List<Product> products; 

            products = (itemsOnPage>0)?await _context.Product.Where(e => e.CategoryId == id && e.Visible == true).Take(itemsOnPage).ToListAsync() : await _context.Product.Where(e => e.CategoryId == id && e.Visible == true).ToListAsync();

            Category category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);
            ViewData["CategoryName"] = category.Name;
            foreach (Product product in products)
            {
                index = new IndexViewModel();
                index.product = product;
                if (ViewData["Language"].Equals("PL")) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                                 Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                else if (ViewData["Language"].Equals("DE")) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "EUR").FirstOrDefault();
                else if (ViewData["Language"].Equals("GB")) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "GBP").FirstOrDefault();
                else index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                   Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();

                if (index.price == null) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                           Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                if (index.price == null)
                {
                    index.price = new ProductPrice()
                    {
                        Price = 0,
                        ProductId = 0,
                        Description = ""
                    };
                }
                index.productLink = ImageService.GetImage(product.Sku, 240, 240);
                indexViewModels.Add(index);
            }

            ViewData["CategoryId"] = category.CategoryId;

            return View(indexViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
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

        public IActionResult Promo()
        {
            List<IndexViewModel> indexViewModels = new List<IndexViewModel>();
            IndexViewModel index;
            List<Product> products = (from product in _context.Product
                                      where product.HavePromoPrice == true
                                      select product).ToList();

            foreach (Product product in products)
            {
                index = new IndexViewModel();
                index.product = product;
                if (ViewData["Language"].Equals("PL")) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                                 Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                else if (ViewData["Language"].Equals("DE")) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "GBP").FirstOrDefault();
                else if (ViewData["Language"].Equals("GB")) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "EUR").FirstOrDefault();
                else index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                   Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();

                if (index.price == null) index.price = _context.Entry(product).Collection(c => c.ProductPrice).Query().
                                           Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                if (index.price == null)
                {
                    index.price = new ProductPrice()
                    {
                        Price = 0,
                        ProductId = 0,
                        Description = ""
                    };
                }
                index.productLink = ImageService.GetImage(product.Sku, 240, 240);
                indexViewModels.Add(index);
            }

            return View(indexViewModels);
        }

        public IActionResult GeneratePDF(int id)
        {
            return RedirectToAction("Index", new {id=id});
        }
    }
}
