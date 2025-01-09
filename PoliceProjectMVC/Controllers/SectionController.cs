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
    public class SectionController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Section Crud Operation
        public ActionResult Index()
        {
            List<Section> sections = db.Sections.ToList();
            return View(sections);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {
                section.IsActive = true;
                section.CreatedDate = DateTime.Now;
                //section.CreatedBy = User.Identity.Name;
                section.CreatedBy = User.Identity.Name;
                db.Sections.Add(section);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(section);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        [HttpPost]
        public ActionResult Edit(Section section)
        {
            if (ModelState.IsValid)
            {

                section.UpdatedDate = DateTime.Now;
                //section.UpdatedBy = User.Identity.Name;
                section.UpdatedBy = User.Identity.Name;;
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(section);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            db.Sections.Remove(section);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}