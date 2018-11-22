using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;

namespace MyWatchWatch.Controllers
{
    public class HomeController : Controller
    {
        MyWatchWatchEntities db = new MyWatchWatchEntities();
        public ActionResult Index()
        {
            var lstProduct1 = db.Products.Where(n => n.ProductStatus == true && n.Style == 1);
            ViewBag.ListProduct1 = lstProduct1;

            var lstProduct2 = db.Products.Where(n => n.ProductStatus == true && n.Style == 2);
            ViewBag.ListProduct2 = lstProduct2;

            var lstProduct3 = db.Products.Where(n => n.ProductStatus == true && n.Style == 3);
            ViewBag.ListProduct3 = lstProduct3;
           
            return View();
        }
        public ActionResult Details(int id = 0, string name = "")
        {
            var viewModel = db.Products.Find(id);
            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var productDetails = db.Products
                .Include("ImgProducts")
                .SingleOrDefault(item => item.ProductId == id);

            if (productDetails == null)
            {
                return View("404");
            }

            var exceptedEntities = db.Products.Where(result => result.ProductId == viewModel.ProductId);
            ViewBag.RelatedProduct =
                db.Products.Where(item => item.CategoryId == viewModel.CategoryId).Except(exceptedEntities).ToList();
            return View(productDetails);
        }



        public ActionResult _slider()
        {
            return PartialView();
        }
        public ActionResult _partiview1()
        {
            return PartialView();
        }
        public ActionResult _product1()
        {
            return PartialView();
        }
        public ActionResult _product2()
        {
            return PartialView();
        }
        public ActionResult _product3()
        {
            return PartialView();
        }

    }
}