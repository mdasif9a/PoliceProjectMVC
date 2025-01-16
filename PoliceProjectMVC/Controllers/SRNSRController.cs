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

    public class SRNSRController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //json data
        public JsonResult GetSMHead(int id)
        {
            var sMHeads = db.SMHeads.AsNoTracking().Where(x => x.MHeadId == id).Select(x => new
            {
                x.Id,
                x.Name_EN
            }).OrderBy(x => x.Name_EN).ToList();
            return Json(sMHeads, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCircles(int id)
        {
            var circles = db.Circles.AsNoTracking().Where(x => x.SubDivId == id).Select(x => new
            {
                x.Id,
                x.Name_En
            }).OrderBy(x => x.Name_En).ToList();
            return Json(circles, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStations(int id)
        {
            var policeStations = db.PoliceStations.AsNoTracking().Where(x => x.CircleId == id).Select(x => new
            {
                x.Id,
                x.Name_En
            }).OrderBy(x => x.Name_En).ToList();
            return Json(policeStations, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModusOpernadi(int id)
        {
            var modusoperandi = db.MOperandis.AsNoTracking().Where(x => x.SMHeadId == id).Select(x => new
            {
                x.Id,
                x.Name_EN
            }).OrderBy(x => x.Name_EN).ToList();
            return Json(modusoperandi, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAccused(Accused accused)
        {
            if (accused.ConfessionFile != null && accused.ConfessionFile.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/ConFession/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(accused.ConfessionFile.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                accused.ConfessionFile.SaveAs(fullPath);
                accused.ConfessionStatement = $"/Images/SRNSRImages/ConFession/{fileName}";
            }

            if (accused.AccusedFile != null && accused.AccusedFile.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/AccusedFile/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(accused.AccusedFile.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                accused.AccusedFile.SaveAs(fullPath);
                accused.ImageOfAccused = $"/Images/SRNSRImages/AccusedFile/{fileName}";
            }

            accused.IsActive = true;
            accused.CreatedBy = User.Identity.Name;
            accused.CreatedDate = DateTime.Now;

            db.Accuseds.Add(accused);
            db.SaveChanges();
            return Json(accused.Id);
        }
        public ActionResult Index()
        {
            List<Act> acts = db.Acts.ToList();
            return View(acts);
        }

        public ActionResult Create()
        {
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SRNSRCase model, string AccusedIds)
        {

            var accusedIdList = AccusedIds.Split(',').Select(int.Parse).ToList();
            var result = db.Accuseds.Where(x => accusedIdList.Contains(x.Id)).ToList();

            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("Accused")).ToList();
            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
                ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                return View(model);
            }

            try
            {

                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.CaseDocument = $"/Images/SRNSRImages/{fileName}";
                }

                model.IsActive = true;
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;

                db.SRNSRCases.Add(model);
                db.SaveChanges();

                int srnsrid = model.Id;

                foreach (var item in result)
                {
                    item.SRNSRCaseId = srnsrid;
                }
                db.SaveChanges();


                TempData["response"] = "Created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
                ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                return View(model);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SRNSRCase model = db.SRNSRCases.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Accuseds = db.Accuseds.Where(x => x.SRNSRCaseId == id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Act model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = User.Identity.Name;
                model.UpdatedDate = DateTime.Now;


                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/ActImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.ImageUrl = $"/Images/ActImages/{fileName}";
                }

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
            Act model = db.Acts.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Acts.Remove(model);
            db.SaveChanges();
            TempData["response"] = "Deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}