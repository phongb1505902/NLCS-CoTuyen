using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWatchWatch.Models;

namespace MyWatchWatch.Areas.Management.Controllers
{
    public class EmployeesController : BaseController
    {
        private MyWatchWatchEntities db = new MyWatchWatchEntities();

        // GET: Management/Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Role);
            return View(employees.ToList());
        }

        // GET: Management/Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Management/Employees/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Management/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeCode,EmployeePass,LastName,FirstName,EmployeeGender,BirthDate,EmployeeEmail,EmployeeAddress,RoleId,Create_Emp,Status_Emp")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    employee.EmployeePass = Encrypt.MD5_Encode(employee.EmployeePass);
                    employee.Create_Emp = DateTime.Now;
                    employee.Status_Emp = true;
                    db.Employees.Add(employee);
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

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", employee.RoleId);
            return View(employee);
        }

        // GET: Management/Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", employee.RoleId);
            return View(employee);
        }

        // POST: Management/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeCode,EmployeePass,LastName,FirstName,EmployeeGender,BirthDate,EmployeeEmail,EmployeeAddress,RoleId,Create_Emp,Status_Emp")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(employee).State = EntityState.Modified;
                    employee.Create_Emp = DateTime.Now;
                    employee.Status_Emp = true;
                    employee.EmployeePass = Encrypt.MD5_Encode(employee.EmployeePass);
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
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", employee.RoleId);
            return View(employee);
        }

        // GET: Management/Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Management/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            try
            {
                db.Employees.Remove(employee);
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
        public ActionResult UploadEmployees(string id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadEmployees(HttpPostedFileBase file, string id)
        {
            var defaultFolderToSaveFile = "~/MyImg/Emp/" + id + "/";

            // Kiểm tra nếu chưa tồn tại thư mục trên thì tạo mới. 
            if (Directory.Exists(Server.MapPath(defaultFolderToSaveFile)) == false)
            {
                Directory.CreateDirectory(Server.MapPath(defaultFolderToSaveFile));
            }

            if (ModelState.IsValid)
            {
                // Kiểm tra nếu người dùng có chọn file
                if (file != null && file.ContentLength > 0)
                {
                    // Lấy tên file
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != null)
                    {
                        var path = Path.Combine(Server.MapPath(defaultFolderToSaveFile), fileName);

                        var i = 1;
                        while (System.IO.File.Exists(path))
                        {
                            path = Path.Combine(Server.MapPath(defaultFolderToSaveFile), i + "_" + fileName);
                            i++;
                        }

                        // Upload file lên Server ở thư mục ~/myImg/
                        file.SaveAs(path);

                        // Lấy imageurl để lưu vào database, có định dạng "~/myImg/Vehicle/Id/ten_file.jpg"
                        var imageUrl = defaultFolderToSaveFile + fileName;

                        // Lưu thông tin image url vào product


                        //var vehicles = db.Vehicles.Find(id);
                        try
                        {
                            var emp = db.Employees.SingleOrDefault(item => item.EmployeeCode == id);
                            emp.EmployeImg = imageUrl;
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
                }
            }
            return View();
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
