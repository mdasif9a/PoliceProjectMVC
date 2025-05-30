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
    public class MissingMobileController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        public ActionResult Index()
        {
            List<MissingMobile> missingMobiles = db.MissingMobiles.OrderByDescending(x => x.CreatedDate).ToList();
            return View(missingMobiles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MissingMobile model)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(model);
            }

            try
            {

                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/MissingMobileImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.DocumentPath = $"/Images/MissingMobileImages/{fileName}";
                }

                model.IsActive = true;
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;

                db.MissingMobiles.Add(model);
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
            MissingMobile model = db.MissingMobiles.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MissingMobile model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = User.Identity.Name;
                model.UpdatedDate = DateTime.Now;


                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/MissingMobileImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.DocumentPath = $"/Images/MissingMobileImages/{fileName}";
                }

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data validation failed.";
            return View(model);
        }

        [HttpPost]
        public ActionResult NewEdit(MissingMobile model)
        {
            MissingMobile mobile = db.MissingMobiles.Find(model.Id);
            db.Entry(model).State = EntityState.Detached;
            mobile.Status = model.Status;
            mobile.UpdatedBy = User.Identity.Name;
            mobile.UpdatedDate = DateTime.Now;
            db.Entry(mobile).State = EntityState.Modified;
            db.SaveChanges();
            TempData["response"] = "Updated successfully.";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissingMobile model = db.MissingMobiles.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.MissingMobiles.Remove(model);
            db.SaveChanges();
            TempData["response"] = "Deleted successfully.";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissingMobile model = db.MissingMobiles.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

    }
}