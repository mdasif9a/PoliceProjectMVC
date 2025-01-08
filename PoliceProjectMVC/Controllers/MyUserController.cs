using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Controllers
{
    public class MyUserController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();
        //User Crud Operation
        public ActionResult Index()
        {
            List<ViewLogin> logins = db.Database.SqlQuery<ViewLogin>("select * from viewlogin").ToList();
            return View(logins);
        }
        public ActionResult Create()
        {
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblLogin login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
                TempData["responseError"] = "Data validation failed.";
                return View(login);
            }

            try
            {
                // Handle file upload if present
                if (login.MyImage != null && login.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/UserProfileImg/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(login.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    login.MyImage.SaveAs(fullPath);
                    login.ImageUrl = $"/Images/UserProfileImg/{fileName}";
                }

                // Set audit fields
                //login.CreatedBy = "admin";
                login.CreatedBy = "admin";
                login.CreatedDate = DateTime.Now;

                // Save to database
                db.TblLogins.Add(login);
                db.SaveChanges();

                TempData["response"] = "User created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(login);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblLogin login = db.TblLogins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View(login);
        }

        [HttpPost]
        public ActionResult Edit(TblLogin login)
        {
            if (ModelState.IsValid)
            {
                login.UpdatedBy = "admin";
                login.UpdatedDate = DateTime.Now;
                if (login.MyImage != null && login.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/UserProfileImg/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(login.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    login.MyImage.SaveAs(fullPath);
                    login.ImageUrl = $"/Images/UserProfileImg/{fileName}";
                }

                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "User Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            TempData["responseError"] = "Data Error.";
            return View(login);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblLogin login = db.TblLogins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            db.TblLogins.Remove(login);
            db.SaveChanges();
            TempData["response"] = "User Deleted Successfully.";
            return RedirectToAction("Index");
        }



        //Role Crud Operation
        public ActionResult RoleIndex()
        {
            List<TblRole> roles = db.TblRoles.ToList();
            return View(roles);
        }
        public ActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RoleCreate(TblRole role)
        {
            role.RoleName = role.RoleName.Trim();
            bool exists = db.TblRoles.Any(x => x.RoleName == role.RoleName);
            if (ModelState.IsValid && !exists)
            {
                role.IsActive = true;
                role.CreatedDate = DateTime.Now;
                //role.CreatedBy = User.Identity.Name;
                role.CreatedBy = "admin";
                db.TblRoles.Add(role);
                db.SaveChanges();
                TempData["response"] = "Role Created Successfully.";
                return RedirectToAction("RoleIndex");
            }
            else if (exists)
            {
                TempData["responseError"] = "Role Already Exists.";
            }
            else
            {
                TempData["responseError"] = "Data Error.";
            }
            return View(role);
        }

        public ActionResult RoleEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRole role = db.TblRoles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        public ActionResult RoleEdit(TblRole role)
        {
            if (ModelState.IsValid)
            {

                role.UpdatedDate = DateTime.Now;
                //role.UpdatedBy = User.Identity.Name;
                role.UpdatedBy = "admin";
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Role Updated Successfully.";
                return RedirectToAction("RoleIndex");
            }
            TempData["responseError"] = "Data Error.";
            return View(role);
        }

        public ActionResult RoleDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblRole role = db.TblRoles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            db.TblRoles.Remove(role);
            db.SaveChanges();
            TempData["response"] = "Role Deleted Successfully.";
            return RedirectToAction("RoleIndex");
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