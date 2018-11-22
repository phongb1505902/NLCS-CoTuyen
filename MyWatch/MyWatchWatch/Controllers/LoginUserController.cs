using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;

namespace MyWatchWatch.Controllers
{
    public class LoginUserController : Controller
    {
        public static bool CheckRegisterU(string username, string password)
        {
            var encrpytedPassword = Encrypt.MD5_Encode(password);
            using (MyWatchWatchEntities db = new MyWatchWatchEntities())
            {
                var singin = from p in db.Customers
                             where p.CustomerCode == username && p.CustomerPass == encrpytedPassword
                             select p;
                return singin.Any();
            }
        }
        public ActionResult LoginU()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginU(LoginU model)
        {
            if (ModelState.IsValid)
            {
                var result = CheckRegisterU(model.CustomerCode, model.CustomerPass);
                if (result)
                {
                    Session["username"] = model.CustomerCode;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("LoginU");
                }
            }
            return View("LoginU");
        }


        public ActionResult LoginCart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginCart(LoginU model)
        {
            if (ModelState.IsValid)
            {
                var result = CheckRegisterU(model.CustomerCode, model.CustomerPass);
                if (result)
                {
                    Session["username"] = model.CustomerCode;
                    return RedirectToAction("ConfirmCheckOut", "Cart");
                }
                else
                {
                    return RedirectToAction("LoginCart");
                }
            }
            return View("LoginCart");
        }

        public ActionResult LogOff()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}