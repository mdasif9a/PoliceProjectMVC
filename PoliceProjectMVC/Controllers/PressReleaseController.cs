using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace PoliceProjectMVC.Controllers
{
    public class PressReleaseController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //PressRelease Crud Operation
        public ActionResult Index()
        {
            List<PressRelease> pressreleases = db.PressReleases.ToList();
            return View(pressreleases);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PressRelease pressrelease)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(pressrelease);
            }

            try
            {
                // Handle file upload if present
                if (pressrelease.MyImage != null && pressrelease.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/PressReleaseImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(pressrelease.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    pressrelease.MyImage.SaveAs(fullPath);
                    pressrelease.ImageUrl = $"/Images/PressReleaseImages/{fileName}";
                }

                // Set audit fields
                pressrelease.IsActive = true;
                pressrelease.CreatedBy = User.Identity.Name;
                pressrelease.CreatedDate = DateTime.Now;

                // Save to database
                db.PressReleases.Add(pressrelease);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(pressrelease);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PressRelease pressrelease = db.PressReleases.Find(id);
            if (pressrelease == null)
            {
                return HttpNotFound();
            }
            return View(pressrelease);
        }

        [HttpPost]
        public ActionResult Edit(PressRelease pressrelease)
        {
            if (ModelState.IsValid)
            {
                pressrelease.UpdatedBy = User.Identity.Name; ;
                pressrelease.UpdatedDate = DateTime.Now;
                if (pressrelease.MyImage != null && pressrelease.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/PressReleaseImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(pressrelease.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    pressrelease.MyImage.SaveAs(fullPath);
                    pressrelease.ImageUrl = $"/Images/PressReleaseImages/{fileName}";
                }

                db.Entry(pressrelease).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(pressrelease);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PressRelease pressrelease = db.PressReleases.Find(id);
            if (pressrelease == null)
            {
                return HttpNotFound();
            }
            db.PressReleases.Remove(pressrelease);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}