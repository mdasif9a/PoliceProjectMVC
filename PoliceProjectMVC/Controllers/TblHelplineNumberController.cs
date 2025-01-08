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
    public class TblHelplineNumberController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //TblHelplineNumber Crud Operation
        public ActionResult Index()
        {
            List<TblHelplineNumber> helplines = db.TblHelplineNumbers.ToList();
            return View(helplines);
        }
        public ActionResult Create()
        {
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblHelplineNumber helpline)
        {
            if (ModelState.IsValid)
            {
                helpline.IsActive = true;
                helpline.CreatedDate = DateTime.Now;
                //helpline.CreatedBy = User.Identity.Name;
                helpline.CreatedBy = "admin";
                db.TblHelplineNumbers.Add(helpline);
                db.SaveChanges();
                TempData["response"] = "Helpline Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            TempData["responseError"] = "Data Error.";
            return View(helpline);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblHelplineNumber helpline = db.TblHelplineNumbers.Find(id);
            if (helpline == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View(helpline);
        }

        [HttpPost]
        public ActionResult Edit(TblHelplineNumber helpline)
        {
            if (ModelState.IsValid)
            {

                helpline.UpdatedDate = DateTime.Now;
                //helpline.UpdatedBy = User.Identity.Name;
                helpline.UpdatedBy = "admin";
                db.Entry(helpline).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Helpline Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyRoles = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            TempData["responseError"] = "Data Error.";
            return View(helpline);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblHelplineNumber helpline = db.TblHelplineNumbers.Find(id);
            if (helpline == null)
            {
                return HttpNotFound();
            }
            db.TblHelplineNumbers.Remove(helpline);
            db.SaveChanges();
            TempData["response"] = "Helpline Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}