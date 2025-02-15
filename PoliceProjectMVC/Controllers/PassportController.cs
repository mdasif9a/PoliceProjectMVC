using PoliceProjectMVC.Custome_Helpers;
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
    public class PassportController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Passport Crud Operation
        public ActionResult Index()
        {
            List<Passport> passports = db.Passports.AsNoTracking().Include(x => x.PoliceStation).ToList();
            return View(passports);
        }
        public ActionResult Create()
        {
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrict();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Passport passport)
        {
            if (ModelState.IsValid)
            {
                passport.IsActive = true;
                passport.CreatedDate = DateTime.Now;
                passport.CreatedBy = User.Identity.Name;
                passport.Status = "PENDING";
                db.Passports.Add(passport);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrict();
            TempData["responseError"] = "Data Error.";
            return View(passport);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passport passport = db.Passports.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrict();
            return View(passport);
        }

        [HttpPost]
        public ActionResult Edit(Passport passport)
        {
            if (ModelState.IsValid)
            {

                passport.UpdatedDate = DateTime.Now;
                passport.UpdatedBy = User.Identity.Name;
                db.Entry(passport).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrict();
            TempData["responseError"] = "Data Error.";
            return View(passport);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passport passport = db.Passports.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            db.Passports.Remove(passport);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}