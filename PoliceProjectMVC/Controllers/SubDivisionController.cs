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
    public class SubDivisionController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //SubDivision Crud Operation
        public ActionResult Index()
        {
            List<SubDivision> subdivisions = db.SubDivisions.ToList();
            return View(subdivisions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubDivision subdivision)
        {
            if (ModelState.IsValid)
            {
                subdivision.IsActive = true;
                subdivision.CreatedDate = DateTime.Now;
                //subdivision.CreatedBy = User.Identity.Name;
                subdivision.CreatedBy = "admin";
                db.SubDivisions.Add(subdivision);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(subdivision);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDivision subdivision = db.SubDivisions.Find(id);
            if (subdivision == null)
            {
                return HttpNotFound();
            }
            return View(subdivision);
        }

        [HttpPost]
        public ActionResult Edit(SubDivision subdivision)
        {
            if (ModelState.IsValid)
            {

                subdivision.UpdatedDate = DateTime.Now;
                //subdivision.UpdatedBy = User.Identity.Name;
                subdivision.UpdatedBy = "admin";
                db.Entry(subdivision).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(subdivision);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDivision subdivision = db.SubDivisions.Find(id);
            if (subdivision == null)
            {
                return HttpNotFound();
            }
            db.SubDivisions.Remove(subdivision);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}