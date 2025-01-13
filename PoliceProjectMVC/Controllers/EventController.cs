using System;
using PoliceProjectMVC.Custome_Helpers;
using PoliceProjectMVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Controllers
{
    public class EventController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        public ActionResult Index()
        {
            List<Event> events = db.Events.ToList();
            return View(events);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event model)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(model);
            }

            try
            {


                model.IsActive = true;
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;

                db.Events.Add(model);
                db.SaveChanges();

                TempData["response"] = "Created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(model);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event model = db.Events.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Event model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = User.Identity.Name;
                model.UpdatedDate = DateTime.Now;



                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data validation failed.";
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event model = db.Events.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Events.Remove(model);
            db.SaveChanges();
            TempData["response"] = "Deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}