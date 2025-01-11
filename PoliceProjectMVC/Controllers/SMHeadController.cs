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
    public class SMHeadController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //SMHead Crud Operation
        public ActionResult Index()
        {
            List<SMHead> smheads = db.SMHeads.ToList();
            return View(smheads);
        }
        public ActionResult Create()
        {
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SMHead smhead)
        {
            if (ModelState.IsValid)
            {
                smhead.IsActive = true;
                smhead.CreatedDate = DateTime.Now;
                smhead.CreatedBy = User.Identity.Name;
                db.SMHeads.Add(smhead);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(smhead);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMHead smhead = db.SMHeads.Find(id);
            if (smhead == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            return View(smhead);
        }

        [HttpPost]
        public ActionResult Edit(SMHead smhead)
        {
            if (ModelState.IsValid)
            {

                smhead.UpdatedDate = DateTime.Now;
                smhead.UpdatedBy = User.Identity.Name;
                db.Entry(smhead).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(smhead);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMHead smhead = db.SMHeads.Find(id);
            if (smhead == null)
            {
                return HttpNotFound();
            }
            db.SMHeads.Remove(smhead);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}