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
    public class CharacterController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Character Crud Operation
        public ActionResult Index()
        {
            List<Character> passports = db.Characters.ToList();
            return View(passports);
        }
        public ActionResult Create()
        {
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Character passport)
        {
            if (ModelState.IsValid)
            {
                passport.IsActive = true;
                passport.CreatedDate = DateTime.Now;
                passport.CreatedBy = User.Identity.Name;
                passport.Status = "PENDING";
                db.Characters.Add(passport);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            TempData["responseError"] = "Data Error.";
            return View(passport);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character passport = db.Characters.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyPolices = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            return View(passport);
        }

        [HttpPost]
        public ActionResult Edit(Character passport)
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
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            TempData["responseError"] = "Data Error.";
            return View(passport);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character passport = db.Characters.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            db.Characters.Remove(passport);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}