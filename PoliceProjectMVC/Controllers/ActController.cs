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
    public class ActController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        public ActionResult Index()
        {
            List<Act> acts = db.Acts.ToList();
            return View(acts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Act model)
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

                model.IsActive = true;
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;

                db.Acts.Add(model);
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
            Act model = db.Acts.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
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