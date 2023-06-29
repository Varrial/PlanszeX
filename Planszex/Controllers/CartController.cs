using Planszex.Models;
using Planszex.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planszex.Controllers
{
    public class CartController : Controller
    {
        public class CartViewModel
        {
            public List<CartProductModel> cartModels = new List<CartProductModel>();
            public List<PopularProductModel> popularProducts = new List<PopularProductModel>();
        }

        public class CartProductModel
        {
            public CartProduct product { get; set; }
            public string productLink { get; set; }
        }

        public class PopularProductModel
        {
            public Product product { get; set; }
            public string productLink { get; set; }
            public ProductPrice price { get; set; }
        }

        private readonly ILogger<CartController> _logger;
        private StoreDbContext _db;

        public CartController(ILogger<CartController> logger, StoreDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        // GET: CartController
        public async Task<ActionResult> IndexAsync()
        {
            CartViewModel cartView = new CartViewModel();

            List<CartProduct> cartProducts = SessionService.GetSession<List<CartProduct>>(HttpContext.Session, "CartProducts");

            if (cartProducts == null) cartProducts = new List<CartProduct>();

            List<CartProductModel> cartViewProducts = new List<CartProductModel>();

            foreach (var cartProduct in cartProducts)
            {
                CartProductModel cartViewModel = new CartProductModel();
                cartViewModel.product = cartProduct;
                cartViewModel.productLink = ImageService.GetImage(cartProduct.Sku, 100, 100);
                cartViewProducts.Add(cartViewModel);
            }

            cartView.cartModels = cartViewProducts;

            List<PopularProductModel> indexViewModels = new List<PopularProductModel>();
            PopularProductModel index;
            var productIds = (from productOrder in _db.ProductOrder
                                      group productOrder by productOrder.ProductId into order
                                      orderby order.Count() descending
                                      select new {ProductID = order.Key, ProductCount = order.Count()}).Take(12).ToList();

            foreach (var item in productIds)
            {
                Product product = await _db.Product.FirstOrDefaultAsync(m => m.ProductId == item.ProductID && m.Visible == true);
                //_db.Entry(product).Collection(c => c.ProductPrice).Query().Where(p => p.ProductId == product.ProductId).;
                index = new PopularProductModel();
                index.product = product;
                if (ViewData["Language"].Equals("PL")) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                                 Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();
                else if (ViewData["Language"].Equals("DE")) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "EUR").FirstOrDefault();
                else if (ViewData["Language"].Equals("GB")) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                                Where(p => p.ProductId == product.ProductId && p.Description == "GBP").FirstOrDefault();
                else index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
                                   Where(p => p.ProductId == product.ProductId && p.Description == "PLN").FirstOrDefault();

                if (index.price == null) index.price = _db.Entry(product).Collection(c => c.ProductPrice).Query().
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

            cartView.popularProducts = indexViewModels;

            return View(cartView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            CartViewModel cartView = new CartViewModel();

            List<CartProduct> cartProducts = SessionService.GetSession<List<CartProduct>>(HttpContext.Session, "CartProducts");

            if (cartProducts == null) return View("Index", new List<CartViewModel>());

            List<CartProductModel> cartViewProducts = new List<CartProductModel>();

            CartProduct toRemove = null;

            foreach (var cartProduct in cartProducts)
            {
                if(cartProduct.Id == id)
                {
                    toRemove = cartProduct;
                    continue;
                }
                CartProductModel cartViewModel = new CartProductModel();
                cartViewModel.product = cartProduct;
                cartViewModel.productLink = ImageService.GetImage(cartProduct.Sku, 100, 100);
                cartViewProducts.Add(cartViewModel);
            }

            cartProducts.Remove(toRemove);

            SessionService.SetSession(HttpContext.Session, "CartProducts", cartProducts);

            cartView.cartModels = cartViewProducts;

            return View("Index", cartView);
        }
    }
}
