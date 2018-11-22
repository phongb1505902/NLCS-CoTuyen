using System;
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
    public class BranchesController : BaseController
    {
        private MyWatchWatchEntities db = new MyWatchWatchEntities();

        // GET: Management/Branches
        public ActionResult Index()
        {
            var branches = db.Branches.Include(b => b.Employee);
            return View(branches.ToList());
        }

        // GET: Management/Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Management/Branches/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "EmployeeCode");
            return View();
        }

        // POST: Management/Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchId,BranchName,BranchDetails,EmployeeCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Branches.Add(branch);
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

            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "EmployeeCode", branch.EmployeeCode);
            return View(branch);
        }

        // GET: Management/Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "EmployeeCode", branch.EmployeeCode);
            return View(branch);
        }

        // POST: Management/Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchId,BranchName,BranchDetails,EmployeeCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(branch).State = EntityState.Modified;
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
            ViewBag.EmployeeCode = new SelectList(db.Employees, "EmployeeCode", "EmployeeCode", branch.EmployeeCode);
            return View(branch);
        }

        // GET: Management/Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Management/Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            try
            {
                db.Branches.Remove(branch);
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
