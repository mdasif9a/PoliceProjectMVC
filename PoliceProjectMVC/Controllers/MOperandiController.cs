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
    public class MOperandiController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //MOperandi Crud Operation
        public ActionResult Index()
        {
            List<MOperandi> moperandis = db.MOperandis.Include(x => x.MHead).Include(x => x.SMHead).ToList();
            return View(moperandis);
        }
        public ActionResult Create()
        {
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            ViewBag.MySMHead = new SelectList(db.SMHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(MOperandi moperandi)
        {
            if (ModelState.IsValid)
            {
                moperandi.IsActive = true;
                moperandi.CreatedDate = DateTime.Now;
                moperandi.CreatedBy = User.Identity.Name;
                db.MOperandis.Add(moperandi);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MySMHead = new SelectList(db.SMHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(moperandi);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOperandi moperandi = db.MOperandis.Find(id);
            if (moperandi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MySMHead = new SelectList(db.SMHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            return View(moperandi);
        }

        [HttpPost]
        public ActionResult Edit(MOperandi moperandi)
        {
            if (ModelState.IsValid)
            {

                moperandi.UpdatedDate = DateTime.Now;
                moperandi.UpdatedBy = User.Identity.Name;
                db.Entry(moperandi).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MySMHead = new SelectList(db.SMHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(moperandi);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOperandi moperandi = db.MOperandis.Find(id);
            if (moperandi == null)
            {
                return HttpNotFound();
            }
            db.MOperandis.Remove(moperandi);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}