﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;

namespace MyWatchWatch.Areas.Management.Controllers
{
    public class PromotionsController : BaseController
    {
        private MyWatchWatchEntities db = new MyWatchWatchEntities();

        // GET: Management/Promotions
        public ActionResult Index()
        {
            return View(db.Promotions.ToList());
        }

        // GET: Management/Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // GET: Management/Promotions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Management/Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromotionId,PromotionName,PromotionDetails,PromotionDiscount,PromotionStatus,PromotionOpen,PromotionClose")] Promotion promotion)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Can not add Promotions");
                return View();
            }
            if (!String.IsNullOrEmpty(promotion.PromotionName))
            {

                DateTime dt1 = DateTime.Parse(promotion.PromotionOpen.ToString());
                DateTime dt2 = DateTime.Parse(promotion.PromotionClose.ToString());
                DateTime dt3 = DateTime.Parse(DateTime.Now.ToString());
                if (dt1.Date < dt3.Date)
                {
                    ModelState.AddModelError("", "Time Promotion Open > Date Time Now ");

                }
                else if (dt1.Date > dt2.Date)
                {
                    ModelState.AddModelError("", "End time must be greater than start time");
                }
                else
                {
                    promotion.PromotionStatus = true;
                    db.Promotions.Add(promotion);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Promotions");
                }
            }
            return View(promotion);
        }

        // GET: Management/Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Management/Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromotionId,PromotionName,PromotionDetails,PromotionDiscount,PromotionStatus,PromotionOpen,PromotionClose")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(promotion).State = EntityState.Modified;
                    promotion.PromotionStatus = true;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Management/Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Management/Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            try
            {
                db.Promotions.Remove(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["msg1"] = "<script>alert('Can not Delete Record');</script>";
                e.ToString();
            }
            return RedirectToAction("Index");
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
