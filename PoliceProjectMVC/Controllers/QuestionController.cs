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
    public class QuestionController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Question Crud Operation
        public ActionResult Index()
        {
            List<Question> passports = db.Questions.ToList();
            return View(passports);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Question passport)
        {
            if (ModelState.IsValid)
            {
                passport.IsActive = true;
                passport.CreatedDate = DateTime.Now;
                passport.CreatedBy = User.Identity.Name;
                db.Questions.Add(passport);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(passport);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question passport = db.Questions.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            return View(passport);
        }

        [HttpPost]
        public ActionResult Edit(Question passport)
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
            TempData["responseError"] = "Data Error.";
            return View(passport);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question passport = db.Questions.Find(id);
            if (passport == null)
            {
                return HttpNotFound();
            }
            db.Questions.Remove(passport);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}