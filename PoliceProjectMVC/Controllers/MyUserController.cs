using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Controllers
{
    public class MyUserController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();
        // GET: MyUser
        public ActionResult Index()
        {
            List<TblLogin> logins = db.TblLogins.ToList();
            return View(logins);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblLogin login)
        {
            if (ModelState.IsValid)
            {
                db.TblLogins.Add(login);
                db.SaveChanges();
                TempData["response"] = "User Created Successfully.";
                return RedirectToAction("Index");
            }
            TempData["response"] = "Data Error.";
            return View(login);
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
            return View(login);
        }

        [HttpPost]
        public ActionResult Edit(TblLogin login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "User Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["response"] = "Data Error.";
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