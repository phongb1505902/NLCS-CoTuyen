using MyWatchWatch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWatchWatch.Controllers
{
    public class ContactController : Controller
    {
        private MyWatchWatchEntities db = new MyWatchWatchEntities();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "ContactId,CompanyName,ContactName,Address,Region,PostalCode,Phone,ContactsTitle,Fax,Status,Create_Contact")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    contact.Create_Contact = DateTime.Now;
                    contact.Status = false;
                    db.Contacts.Add(contact);
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
                return RedirectToAction("Index","Home");
            }

            return View(contact);
        }
    }
}