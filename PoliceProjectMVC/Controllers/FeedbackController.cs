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
    public class FeedbackController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Feedback Crud Operation
        public ActionResult Index()
        {
            List<Feedback> feedbacks = db.Feedbacks.ToList();
            return View(feedbacks);
        }
        public ActionResult Create()
        {
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.IsActive = true;
                feedback.CreatedDate = DateTime.Now;
                feedback.CreatedBy = User.Identity.Name;
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            TempData["responseError"] = "Data Error.";
            return View(feedback);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            return View(feedback);
        }

        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {

                feedback.UpdatedDate = DateTime.Now;
                feedback.UpdatedBy = User.Identity.Name;
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDistricts = MyDropdownsValue.GetDistrictForSelectList();
            TempData["responseError"] = "Data Error.";
            return View(feedback);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}