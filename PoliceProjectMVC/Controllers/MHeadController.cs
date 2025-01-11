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
    public class MHeadController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //MHead Crud Operation
        public ActionResult Index()
        {
            List<MHead> mheads = db.MHeads.ToList();
            return View(mheads);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MHead mhead)
        {
            if (ModelState.IsValid)
            {
                mhead.IsActive = true;
                mhead.CreatedDate = DateTime.Now;
                mhead.CreatedBy = User.Identity.Name;
                db.MHeads.Add(mhead);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(mhead);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MHead mhead = db.MHeads.Find(id);
            if (mhead == null)
            {
                return HttpNotFound();
            }
            return View(mhead);
        }

        [HttpPost]
        public ActionResult Edit(MHead mhead)
        {
            if (ModelState.IsValid)
            {

                mhead.UpdatedDate = DateTime.Now;
                mhead.UpdatedBy = User.Identity.Name;
                db.Entry(mhead).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(mhead);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MHead mhead = db.MHeads.Find(id);
            if (mhead == null)
            {
                return HttpNotFound();
            }
            db.MHeads.Remove(mhead);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}