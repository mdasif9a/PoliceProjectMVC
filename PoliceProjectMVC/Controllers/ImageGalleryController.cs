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
    public class ImageGalleryController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        public ActionResult Index()
        {
            List<ImageGallery> imagegallerys = db.ImageGalleries.ToList();
            return View(imagegallerys);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ImageGallery model)
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
                    string imagePath = Path.Combine(Server.MapPath("~/Images/ImageGalleryImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.ImageUrl = $"/Images/ImageGalleryImages/{fileName}";
                }

                model.IsActive = true;
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;

                db.ImageGalleries.Add(model);
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
            ImageGallery model = db.ImageGalleries.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ImageGallery model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = User.Identity.Name;
                model.UpdatedDate = DateTime.Now;


                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/ImageGalleryImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.ImageUrl = $"/Images/ImageGalleryImages/{fileName}";
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
            ImageGallery model = db.ImageGalleries.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.ImageGalleries.Remove(model);
            db.SaveChanges();
            TempData["response"] = "Deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}