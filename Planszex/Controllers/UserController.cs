using Planszex.Models;
using Planszex.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Planszex.Controllers
{
    public class UserController : Controller
    {
        public class SignInViewModel
        {

            [Required(ErrorMessage = "To pole jest wymagane!")]
            [MinLength(6, ErrorMessage = "Hasło jest za krótkie!")]

            [DisplayName("Hasło")]
            public String PasswordVM { get; set; }

            public User user { get; set; }
        }

        public class DetailsViewModel
        {
            public User user { get; set; }
            public int itemsOnPage { get; set; }
        }

        public class CheckOrdersViewModel
        {
            public List<Order> orders { get; set; }
        }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [StringLength(30)]
        [DisplayName("Login")]
        public String Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [MinLength(6, ErrorMessage = "Hasło jest za krótkie!")]
        [DisplayName("Hasło")]
        public String Password { get; set; }

        private readonly ILogger<UserController> _logger;
        private StoreDbContext _db;

        public UserController(ILogger<UserController> logger, StoreDbContext db)
        {
            _db = db;
            _logger = logger;
        }
        private User ValidateUser(SignInViewModel model)
        {
            User _user = null;
            if (model.user != null && model.user.Login != null)
            {
                _user = _db.GetUser(model.user.Login, model.PasswordVM);
            }
            else
            {
                _db.GetUser(Login, model.PasswordVM);
            }
            return (_user != null) ? _user : null;
        }
        private User ValidateUser(String login, String password)
        {
            User _user = _db.GetUser(login, password);

            return _user;
        }

        [HttpGet]
        public IActionResult SignIn()
        {

            Console.WriteLine("213123");
            ViewBag.SignIn = TempData["SignIn"];
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SignIn(string Login, string Password)
        {
            Console.WriteLine("SignIn");
            User user = ValidateUser(Login, Password);
            if (user != null)
            {
                List<Claim> claims;
                if (user.Permission == true)
                {
                    claims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name, "Admin")
                    };
                }
                else if(user.Permission == false)
                {
                    claims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name, "Pracownik")
                    };
                }
                else
                {
                    claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, Login)
                    };
                }
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                SessionService.SetSession(HttpContext.Session, "UserID", user.UserId);
                TempData["SignIn"] = "";
                return RedirectToAction("Index", "Home");
            }

            TempData["SignIn"] = "Błąd Logowania!";

            return RedirectToAction("SignIn", "User");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignInViewModel model)
        {
            model.ToString();

            _db.CreateUser(model.user, model.PasswordVM);

            User user = ValidateUser(model);
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, model.user.Login)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                SessionService.SetSession(HttpContext.Session, "UserID", user.UserId);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SignInToCheckout(string Login, string Password)
        {
            User user = ValidateUser(Login, Password);

            if (user != null)
            {
                List<Claim> claims;
                if (user.Permission == true)
                {
                    claims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name, "Admin")
                    };
                }
                else
                {
                    claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, Login)
                    };
                }
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                SessionService.SetSession(HttpContext.Session, "UserID", user.UserId);
                TempData["SignIn"] = "";

                return RedirectToAction("Index", "Checkout");
            }

            TempData["SignIn"] = "Błąd Logowania!";

            return RedirectToAction("SignIn", "User");
        }

        public async Task<IActionResult> SignOut()
        {
            List<Claim> claims = new List<Claim>();
            new Claim(ClaimTypes.Name, "None");
            await HttpContext.SignOutAsync("CookieAuthentication");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Details()
        {
            int userID = SessionService.GetSession<int>(HttpContext.Session, "UserID");
            User user = _db.User.FirstOrDefault(m => m.UserId == userID);

            DetailsViewModel model = new DetailsViewModel();

            model.user = user;

            string iOPstring = SessionService.GetSession<string>(HttpContext.Session, "ItemsOnPage");
            model.itemsOnPage = int.Parse(iOPstring);

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeItemsOnPage(int? itemsOnPage)
        {
            SessionService.SetSession(HttpContext.Session, "ItemsOnPage", itemsOnPage);

            return RedirectToAction("Details");
        }

        public IActionResult CheckOrders()
        {
            CheckOrdersViewModel model = new CheckOrdersViewModel();
            int userID = SessionService.GetSession<int>(HttpContext.Session, "UserID");
            User user = _db.User.FirstOrDefault(m => m.UserId == userID);
            _db.Entry(user).Collection(c => c.Order).Load();
            List<Order> orders = user.Order.ToList();
            model.orders = orders;
            
            return View(model);
        }
    }
}