using RealEstate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly RealEstateContext context;
        public ProductsController(RealEstateContext context) => this.context = context;
        // GET: HomeController
        List<TbProduct> products = new List<TbProduct>();
        TbProduct product = new TbProduct();
        public IActionResult Index()
        {

            products = context.TbProducts.Include(a => a.Category).ToList();
            return View(products);
        }


        // GET: HomeController/Create
        public IActionResult Create()
        {
            ViewBag.state = "New Product";
            ViewBag.enter = "Add";
            ViewBag.Category = context.TbCategories.ToList();
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbProduct Product, List<IFormFile> Files)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Files)
                {
                    if (file.Length > 0)
                    {
                        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                        using (var stream = System.IO.File.Create(filePaths))
                        {
                            await file.CopyToAsync(stream);
                        }
                        Product.ImageName = ImageName;
                    }
                }
                try
                {
                    if (Product.ProductId == 0)
                        context.TbProducts.Add(Product);
                    else
                    {
                        TbProduct p = new TbProduct();
                        p = context.TbProducts.ToList().FirstOrDefault(a => a.ProductId == Product.ProductId);
                        if (Product.ImageName == null)
                            p.ImageName = p.ImageName;
                        else
                            p.ImageName = Product.ImageName;
                        p.ProductName = Product.ProductName;
                        p.ProductBuyPrice = Product.ProductBuyPrice;
                        p.ProductSalePrice = Product.ProductSalePrice;
                        p.CategoryId = Product.CategoryId;

                        //context.Entry(Product).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                    return RedirectToAction();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return View();
                }
            }
            else
            {
                try
                {
                    ViewBag.Category = context.TbCategories.ToList();
                    return View(Product);
                }

                catch (Exception ex)
                {
                    await Response.WriteAsync(ex.ToString());
                    return View();
                }
            }

        }

        // GET: HomeController/Edit/5
        TbProduct pp = new TbProduct();
        public IActionResult Edit(int id)
        {

            ViewBag.state = "Edit Product";
            ViewBag.enter = "Edit";
            ViewBag.Category = context.TbCategories.ToList();
            pp = context.TbProducts.Include(a => a.TbImages).ToList().FirstOrDefault(a => a.ProductId == id);
            return View("Create", pp);
        }


        public IActionResult DeleteProduct(int id)
        {
            try
            {
                product = context.TbProducts.Find(id);
                context.TbProducts.Remove(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
