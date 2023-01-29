using Books.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RealEstate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceWebSite.Controllers
{
    public class OrderController1 : Controller
    {
        private readonly UserManager<IdentityUser> manger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RealEstateContext con;

        public OrderController1(UserManager<IdentityUser> manager, SignInManager<IdentityUser> signInManager, RealEstateContext context)
        {
            this.manger = manager;
            this.signInManager = signInManager;
            con = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Cart()
        //{
        //    ShopingCart oShopingCart = HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart");
        //    return View(oShopingCart);
        //}
        //[HttpPost]
        //public IActionResult RemoveItem(int id)
        //{
        //    ShopingCart oShopingCart = HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart");
        //    oShopingCart.lstItem.Remove(oShopingCart.lstItem.Where(a => a.ProductId == id).FirstOrDefault());
        //    oShopingCart.Total = oShopingCart.lstItem.Sum(a => a.Total);
        //    HttpContext.Session.SetObjectAsJson("Cart", oShopingCart);
        //    return RedirectToAction("Cart");
        //}
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(UserModel User)
        {
            var user = new IdentityUser()
            {
                Email = User.email,
                UserName = User.email,
            };
            var result = await manger.CreateAsync(user, User.password);
            if (result.Succeeded)
                return Redirect("~/");
            else
                return View("Register", User);
        }
        public IActionResult Login(string ReturnUrl)
        {
            return View(new UserModel() { ReturnUrl = ReturnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel User)
        {

            var result = await signInManager.PasswordSignInAsync(User.email, User.password, true, false);
            if (string.IsNullOrEmpty(User.ReturnUrl))
                User.ReturnUrl = "~/";
            if (result.Succeeded)
            {
                return Redirect(User.ReturnUrl);
            }
            else
                return View("Login", User);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
