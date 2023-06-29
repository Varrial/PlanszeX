using Planszex.Models;
using Planszex.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planszex.Controllers
{
    
    public class CheckoutController : Controller
    {
        public class CheckoutProductViewModel
        {
            public CartProduct product { get; set; }
            public string productLink { get; set; }
        }
        public class CheckoutViewModel
        {
            public List<CheckoutProductViewModel> checkoutProducts { get; set; }
            public Order order { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string email { get; set; }

        }

        private readonly ILogger<CheckoutController> _logger;
        private StoreDbContext _db;

        public CheckoutController(ILogger<CheckoutController> logger, StoreDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CartProduct> cartProducts = SessionService.GetSession<List<CartProduct>>(HttpContext.Session, "CartProducts");

            if (cartProducts == null) cartProducts = new List<CartProduct>();

            List<CheckoutProductViewModel> checkoutProducts = new List<CheckoutProductViewModel>();

            foreach (var cartProduct in cartProducts)
            {
                CheckoutProductViewModel checkoutProduct = new CheckoutProductViewModel();
                checkoutProduct.product = cartProduct;
                checkoutProduct.productLink = ImageService.GetImage(cartProduct.Sku, 100, 100);
                checkoutProducts.Add(checkoutProduct);
            }

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();

            checkoutViewModel.checkoutProducts = checkoutProducts;

            int userID = SessionService.GetSession<int>(HttpContext.Session, "UserID");
            if (userID>0)
            {
                User user = _db.User.FirstOrDefault(m => m.UserId == userID);
                checkoutViewModel.name = user.Name;
                checkoutViewModel.surname = user.Surname;
                checkoutViewModel.email = user.Email;
                checkoutViewModel.phone = user.Phone;
                checkoutViewModel.address = user.Address;
            }

            return View(checkoutViewModel);
        }

        [HttpPost]
        public IActionResult HandleTransaction(CheckoutViewModel checkoutViewModel)
        {
            Order order = new Order();
            order.Address = checkoutViewModel.address;
            order.Price = 199.99M;
            order.PriceDescription = "PLN";
            order.PaymentMethod = checkoutViewModel.order.PaymentMethod;
            order.OrderDate = System.DateTime.Today;
            order.Status = "Potwierdzone";
            order.Shipping = checkoutViewModel.order.Shipping;
            order.UserName = checkoutViewModel.name;
            order.UserSurname = checkoutViewModel.surname;

            int userID = SessionService.GetSession<int>(HttpContext.Session, "UserID");
            if (userID > 0)
            {
                User user = _db.User.FirstOrDefault(m => m.UserId == userID);
                order.User = user;
            }

            _db.Add(order);
            _db.SaveChanges();


            List<CartProduct> cartProducts = SessionService.GetSession<List<CartProduct>>(HttpContext.Session, "CartProducts");

            if (cartProducts == null) cartProducts = new List<CartProduct>();

            List<Product> products = new List<Product>();

            foreach (var cartProduct in cartProducts)
            {
                Product product = _db.Product.FirstOrDefault(m => m.ProductId == cartProduct.Id);

                ProductOrder productOrder = new ProductOrder();
                productOrder.ProductId = product.ProductId;
                productOrder.OrderId = order.OrderId;
                productOrder.Qty = cartProduct.Qty;
                _db.Add(productOrder);
                _db.SaveChanges();
            }


            return View();
        }

        public IActionResult DescribeBuyer()
        {
            int userID = SessionService.GetSession<int>(HttpContext.Session, "UserID");
            if (userID > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
