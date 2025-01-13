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
    public class DesignationController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Designation Crud Operation
        public ActionResult Index()
        {
            List<Designation> designations = db.Designations.ToList();
            return View(designations);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Designation designation)
        {
            if (ModelState.IsValid)
            {
                designation.IsActive = true;
                designation.CreatedDate = DateTime.Now;
                //designation.CreatedBy = User.Identity.Name;
                designation.CreatedBy = User.Identity.Name;
                db.Designations.Add(designation);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(designation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        [HttpPost]
        public ActionResult Edit(Designation designation)
        {
            if (ModelState.IsValid)
            {

                designation.UpdatedDate = DateTime.Now;
                //designation.UpdatedBy = User.Identity.Name;
                designation.UpdatedBy = User.Identity.Name; ;
                db.Entry(designation).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(designation);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            db.Designations.Remove(designation);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}