using RealEstate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly RealEstateContext con;

        public CategoriesController(RealEstateContext context) => con = context;
        List<TbCategory> lstCategories = new List<TbCategory>();
        TbCategory category = new TbCategory();
        public IActionResult Index()
        {
            lstCategories = con.TbCategories.Include(a => a.TbProducts).ToList();
            return View(lstCategories);
        }
        public IActionResult Create()
        {
            ViewBag.state = "New Category";
            ViewBag.enter = "Add";
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbCategory Category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Category.CategoryId == 0)
                        con.TbCategories.Add(Category);
                    else
                        con.Entry(Category).State = EntityState.Modified;
                    con.SaveChanges();
                    return RedirectToAction();
                }
                else
                    return View(Category);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.state = "Edit Product";
            ViewBag.enter = "Edit";
            category = con.TbCategories.Find(id);
            return View("Create", category);
        }
        public IActionResult DeleteCategory(int id)
        {
            con.Remove(con.TbCategories.Find(id));
            con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
