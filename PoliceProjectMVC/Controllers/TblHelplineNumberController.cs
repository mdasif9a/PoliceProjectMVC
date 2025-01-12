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
    public class TblHelplineNumberController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //TblHelplineNumber Crud Operation
        public ActionResult Index()
        {
            List<TblHelplineNumber> helplines = db.TblHelplineNumbers.Include(x => x.Designation).ToList();
            return View(helplines);
        }
        public ActionResult Create()
        {
            ViewBag.MyHelplineType = MyDropdownsValue.GetHelplineType();
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblHelplineNumber helpline)
        {
            if (ModelState.IsValid)
            {
                helpline.IsActive = true;
                helpline.CreatedDate = DateTime.Now;
                helpline.CreatedBy = User.Identity.Name;
                db.TblHelplineNumbers.Add(helpline);
                db.SaveChanges();
                TempData["response"] = "Helpline Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyHelplineType = MyDropdownsValue.GetHelplineType();
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
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
            ViewBag.MyHelplineType = MyDropdownsValue.GetHelplineType();
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View(helpline);
        }

        [HttpPost]
        public ActionResult Edit(TblHelplineNumber helpline)
        {
            if (ModelState.IsValid)
            {

                helpline.UpdatedDate = DateTime.Now;
                helpline.UpdatedBy = User.Identity.Name;
                db.Entry(helpline).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Helpline Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyHelplineType = MyDropdownsValue.GetHelplineType();
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
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