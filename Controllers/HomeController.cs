using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RealEstateContext context;
                
        public HomeController(ILogger<HomeController> logger, RealEstateContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            //ViewBag.slider = context.TbProducts.Include(e=>e.Category).Take(3).ToList();
            return View(context.TbProducts.Include(e=>e.Category).Take(3).ToList());
        }

        public IActionResult Buy()
        {
            return View(context.TbProducts.Include(e => e.Category).Where(a=>a.Category.CategoryName=="Buy").ToList());
        }

        public IActionResult Rent()
        {
            return View(context.TbProducts.Include(e => e.Category).Where(a => a.Category.CategoryName == "Rent").ToList());
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
