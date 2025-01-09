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
    public class DSPController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //DSP Crud Operation
        public ActionResult Index()
        {
            List<DSP> dsps = db.DSPs.ToList();
            return View(dsps);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DSP dsp)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(dsp);
            }

            try
            {
                // Handle file upload if present
                if (dsp.MyImage != null && dsp.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/DSPImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(dsp.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    dsp.MyImage.SaveAs(fullPath);
                    dsp.ImageUrl = $"/Images/DSPImages/{fileName}";
                }

                // Set audit fields
                //dsp.CreatedBy = "admin";
                dsp.CreatedBy = "admin";
                dsp.CreatedDate = DateTime.Now;

                // Save to database
                db.DSPs.Add(dsp);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(dsp);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSP dsp = db.DSPs.Find(id);
            if (dsp == null)
            {
                return HttpNotFound();
            }
            return View(dsp);
        }

        [HttpPost]
        public ActionResult Edit(DSP dsp)
        {
            if (ModelState.IsValid)
            {
                dsp.UpdatedBy = "admin";
                dsp.UpdatedDate = DateTime.Now;
                if (dsp.MyImage != null && dsp.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/DSPImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(dsp.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    dsp.MyImage.SaveAs(fullPath);
                    dsp.ImageUrl = $"/Images/DSPImages/{fileName}";
                }

                db.Entry(dsp).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(dsp);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSP dsp = db.DSPs.Find(id);
            if (dsp == null)
            {
                return HttpNotFound();
            }
            db.DSPs.Remove(dsp);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}