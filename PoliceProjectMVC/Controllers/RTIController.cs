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
    public class RTIController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //RTI Crud Operation
        public ActionResult Index()
        {
            List<RTI> rtis = db.RTIs.ToList();
            return View(rtis);
        }

        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(RTI rti)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
                TempData["responseError"] = "Data validation failed.";
                return View(rti);
            }

            try
            {
                // Handle file upload if present
                if (rti.MyImage != null && rti.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/RTIImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(rti.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    rti.MyImage.SaveAs(fullPath);
                    rti.ImageUrl = $"/Images/RTIImages/{fileName}";
                }

                // Set audit fields
                rti.IsActive = true;
                rti.CreatedBy = User.Identity.Name;
                rti.CreatedDate = DateTime.Now;

                // Save to database
                db.RTIs.Add(rti);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(rti);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTI rti = db.RTIs.Find(id);
            if (rti == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View(rti);
        }

        [HttpPost]
        public ActionResult Edit(RTI rti)
        {
            if (ModelState.IsValid)
            {
                rti.UpdatedBy = User.Identity.Name; ;
                rti.UpdatedDate = DateTime.Now;
                if (rti.MyImage != null && rti.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/RTIImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(rti.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    rti.MyImage.SaveAs(fullPath);
                    rti.ImageUrl = $"/Images/RTIImages/{fileName}";
                }

                db.Entry(rti).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(rti);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTI rti = db.RTIs.Find(id);
            if (rti == null)
            {
                return HttpNotFound();
            }
            db.RTIs.Remove(rti);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}