using MyWatchWatch.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyWatchWatch.Areas.Management.Controllers
{
    public class PageController : BaseController
    {
        private MyWatchWatchEntities db = new MyWatchWatchEntities();
        // GET: Management/Page
        public ActionResult Page()
        {
            return View();
        }
        public ActionResult _countProducts()
        {
            var countProducts = db.Products.Count();
            ViewBag.countProducts = countProducts;
            return PartialView();
        }
        public ActionResult _statusPromotin()
        {
            var statusPromotin = (from d in db.Promotions where d.PromotionStatus == true select d).Count();
            ViewBag.statusPromotin = statusPromotin;
            return PartialView();
        }
        public ActionResult _countCustomer()
        {
            var countCus = db.Customers.Count();
            ViewBag.countCus = countCus;
            return PartialView();
        }
        public ActionResult _CountNews()
        {
            var countNews = db.News.Count();
            ViewBag.countNews = countNews;
            return PartialView();
        }
        public ActionResult _countCart()
        {
            var countCarts = (from p in db.Orders select p).Count();

            ViewBag.countCarts = countCarts;
            return PartialView();
        }
        public ActionResult _countContacts()
        {
            var statusContacts = (from p in db.Contacts where p.Status == false select p).Count();

            ViewBag.statusContacts = statusContacts;
            return PartialView();
        }

        public ActionResult ChartColumn()
        {
           
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from p in db.Orders
                           join p1 in db.OrderDetails on p.OrderId equals p1.OrderId select new { Order = p.OrderDate, OrderDetail = p1.SoldPrice} );
            results.ToList().ForEach(rs => xValue.Add(rs.Order));
            results.ToList().ForEach(rs => yValue.Add(rs.OrderDetail));

            new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla3D).AddTitle("").AddSeries("Default", chartType: "column", xValue: xValue, yValues: yValue).Write("bmp");
            return null;

        }
    }
}