using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;
using Rotativa;

namespace MyWatchWatch.Areas.Management.Controllers
{
    public class OrdersController : BaseController
    {
        private MyWatchWatchEntities db = new MyWatchWatchEntities();

        // GET: Management/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.PaymentMethod1);
            return View(orders.ToList());
        }

        // GET: Management/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Management/Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerCode = new SelectList(db.Customers, "CustomerCode", "CustomerPass");
            ViewBag.PaymentMethod = new SelectList(db.PaymentMethods, "PaymentId", "PaymentName");
            return View();
        }

        // POST: Management/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerCode,OrderDate,RequiredDate,OrderAddress,OrderPhone,PaymentMethod,Order_Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerCode = new SelectList(db.Customers, "CustomerCode", "CustomerPass", order.CustomerCode);
            ViewBag.PaymentMethod = new SelectList(db.PaymentMethods, "PaymentId", "PaymentName", order.PaymentMethod);
            return View(order);
        }

        // GET: Management/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerCode = new SelectList(db.Customers, "CustomerCode", "CustomerPass", order.CustomerCode);
            ViewBag.PaymentMethod = new SelectList(db.PaymentMethods, "PaymentId", "PaymentName", order.PaymentMethod);
            return View(order);
        }

        // POST: Management/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerCode,OrderDate,RequiredDate,OrderAddress,OrderPhone,PaymentMethod,Order_Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerCode = new SelectList(db.Customers, "CustomerCode", "CustomerPass", order.CustomerCode);
            ViewBag.PaymentMethod = new SelectList(db.PaymentMethods, "PaymentId", "PaymentName", order.PaymentMethod);
            return View(order);
        }

        // GET: Management/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Management/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }
        public ActionResult PrintOrder(int id)
        {
           
                Order order = db.Orders.FirstOrDefault(p => p.OrderId == id);
                var report = new PartialViewAsPdf("Details", order);
                return report;
            
            }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
