
using Planszex.Filters;
using Planszex.Models;
using Planszex.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Planszex.Controllers
{   
    public class HomeController : Controller
    {
        public class IndexViewModel
        {
            public Product product { get; set; }
            public string productLink { get; set; }
            public ProductPrice price { get; set; }
        }

        private readonly ILogger<HomeController> _logger;
        private StoreDbContext _db;

        public HomeController(ILogger<HomeController> logger, StoreDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public ActionResult Index()
        {
            string visited = HttpContext.Session.GetString("Visited");
            if (visited == null)
            {
                _db.IncrementCounter();
                HttpContext.Session.SetString("Visited", "Yes");
                SessionService.SetSession(HttpContext.Session, "ItemsOnPage", "0");
                SessionService.SetSession(HttpContext.Session, "UserID", 0);
            }

            ViewData["Permission"] = true;
            List<IndexViewModel> indexViewModels = new List<IndexViewModel>();
            IndexViewModel index;
            List<Product> products = (from product in _db.Product
                                      where product.Visible == true
                         orderby product.AddDate descending
                         select product).Take(4).ToList();

            foreach (Product product in products)
            {
                //_db.Entry(product).Collection(c => c.ProductPrice).Query().Where(p => p.ProductId == product.ProductId).;
                index = new IndexViewModel();
                index.product = product;
                if(ViewData["Language"].Equals("PL")) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                else if (ViewData["Language"].Equals("DE")) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "EUR").FirstOrDefault();
                else if (ViewData["Language"].Equals("GB")) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "GBP").FirstOrDefault();
                else index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                   Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                
                if(index.price==null) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                        Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                if(index.price == null)
                {
                    index.price = new ProductPrice()
                    {
                        Price = 0,
                        ProductId = 0,
                        Description = ""
                    };
                }


                index.productLink = ImageService.GetImage(product.Sku,240,240);
                indexViewModels.Add(index);
            }
            return View(indexViewModels);
        }

        public ActionResult ChangeLanguage()
        {
            if (Request.Cookies["Language"].Equals("PL")) Response.Cookies.Append("Language", "DE");
            else if (Request.Cookies["Language"].Equals("DE")) Response.Cookies.Append("Language", "GB");
            else Response.Cookies.Append("Language", "PL");
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
