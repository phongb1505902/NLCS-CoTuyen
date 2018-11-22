using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;
using PagedList;

namespace MyWatchWatch.Controllers
{
    public class TypeWatchController : Controller
    {
        MyWatchWatchEntities db = new MyWatchWatchEntities();
        // GET: TypeWatch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Men(string sortOrder, string searchString, int? page, string currentFilter)
        {
            var lstTouris = db.Products
                .Include("Category")
                .Where(u => u.Style == 1 && u.ProductStatus == true && (u.Category.CategoryId == 2
                                             || u.Category.CategoryId == 3
                                             || u.Category.CategoryId == 4
                                             || u.Category.CategoryId == 5
                                             || u.Category.CategoryId == 6
                                             || u.Category.CategoryId == 7));
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                lstTouris = lstTouris.Where(s => s.ProductName.Contains(searchString)
                                       || s.Category.CategoryName.Contains(searchString));
            }
            int PageSize = 4;
            int PageNumber = (page ?? 1);
            return View(lstTouris.OrderBy(n => n.Create_Product).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult Women(string sortOrder, string searchString, int? page, string currentFilter)
        {
            var lstTouris = db.Products
                .Include("Category")
                .Where(u => u.Style == 2&& u.ProductStatus == true && (u.Category.CategoryId == 2
                                             || u.Category.CategoryId == 3
                                             || u.Category.CategoryId == 4
                                             || u.Category.CategoryId == 5
                                             || u.Category.CategoryId == 6
                                             || u.Category.CategoryId == 7));
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                lstTouris = lstTouris.Where(s => s.ProductName.Contains(searchString)
                                       || s.Category.CategoryName.Contains(searchString));
            }
            int PageSize = 12;
            int PageNumber = (page ?? 1);
            return View(lstTouris.OrderBy(n => n.Create_Product).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult Kids(string sortOrder, string searchString, int? page, string currentFilter)
        {
            var lstTouris = db.Products
                .Include("Category")
                .Where(u => u.Style == 3 && u.ProductStatus == true && (u.Category.CategoryId == 2
                                             || u.Category.CategoryId == 3
                                             || u.Category.CategoryId == 4
                                             || u.Category.CategoryId == 5
                                             || u.Category.CategoryId == 6
                                             || u.Category.CategoryId == 7));
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                lstTouris = lstTouris.Where(s => s.ProductName.Contains(searchString)
                                       || s.Category.CategoryName.Contains(searchString));
            }
            int PageSize = 12;
            int PageNumber = (page ?? 1);
            return View(lstTouris.OrderBy(n => n.Create_Product).ToPagedList(PageNumber, PageSize));
        }
    }
}