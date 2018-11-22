using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Data.Entity;
using CaptchaMvc.HtmlHelpers;
using System.Net;
using MyWatchWatch.Models;
using System.Web.Mvc;

namespace MyWatchWatch.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        MyWatchWatchEntities db = new MyWatchWatchEntities();
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "CustomerCode,CustomerPass,CustomerFullName,CustomerAddress,CustomerPhone")] Customer customer)
        {
            if (ModelState.IsValid && this.IsCaptchaValid("Capcha is not valid"))
            {
                try
                {
                    customer.CustomerPass = Encrypt.MD5_Encode(customer.CustomerPass);
                    db.Customers.Add(customer);
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
                catch (Exception ex)
                {
                    TempData["msg"] = "<script>alert('Username is exist. You can write informtion again');</script>";
                    ex.ToString();
                    return RedirectToAction("Register");
                }
                return RedirectToAction("Success", "Register");
            }

            return View(customer);
        }
        public JsonResult CheckCustomer(string Customer)
        {
            return Json(!db.Customers.Any(x => x.CustomerCode == Customer), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult UpdateAccout(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccout([Bind(Include = "CustomerCode,CustomerPass,CustomerFullName,ContactCompany,CustomerAddress,CustomerRegion,CustomerPostalCode,CustomerPhone,CustomerFax")] Customer customer)
        {
            if (ModelState.IsValid && this.IsCaptchaValid("Capcha is not valid"))
            {
                try
                {
                    Session["username"] = customer.CustomerCode;
                    customer.CustomerPass = Encrypt.MD5_Encode(customer.CustomerPass);
                    db.Entry(customer).State = EntityState.Modified;
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
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }

    }
}