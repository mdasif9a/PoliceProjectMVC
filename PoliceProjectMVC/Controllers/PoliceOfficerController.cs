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
    public class PoliceOfficerController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //PoliceOfficer Crud Operation
        public ActionResult Index()
        {
            List<PoliceOfficer> policeofficers = db.PoliceOfficers.Include(x => x.Designation).ToList();
            return View(policeofficers);
        }
        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PoliceOfficer policeofficer)
        {
            if (ModelState.IsValid)
            {
                policeofficer.IsActive = true;
                policeofficer.CreatedDate = DateTime.Now;
                policeofficer.CreatedBy = User.Identity.Name;
                db.PoliceOfficers.Add(policeofficer);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(policeofficer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceOfficer policeofficer = db.PoliceOfficers.Find(id);
            if (policeofficer == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View(policeofficer);
        }

        [HttpPost]
        public ActionResult Edit(PoliceOfficer policeofficer)
        {
            if (ModelState.IsValid)
            {

                policeofficer.UpdatedDate = DateTime.Now;
                policeofficer.UpdatedBy = User.Identity.Name;
                db.Entry(policeofficer).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(policeofficer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceOfficer policeofficer = db.PoliceOfficers.Find(id);
            if (policeofficer == null)
            {
                return HttpNotFound();
            }
            db.PoliceOfficers.Remove(policeofficer);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}