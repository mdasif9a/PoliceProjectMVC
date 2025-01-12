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
    public class PoliceStationController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //PoliceStation Crud Operation
        public ActionResult Index()
        {
            List<PoliceStation> sdpos = db.PoliceStations.ToList();
            return View(sdpos);
        }

        public ActionResult Create()
        {
            ViewBag.MyCircle = new SelectList(db.Circles.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PoliceStation sdpo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyCircle = new SelectList(db.Circles.ToList(), "Id", "Name_En");
                TempData["responseError"] = "Data validation failed.";
                return View(sdpo);
            }

            try
            {
                // Handle file upload if present
                if (sdpo.MyImage != null && sdpo.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/PoliceStationImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(sdpo.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    sdpo.MyImage.SaveAs(fullPath);
                    sdpo.ImageUrl = $"/Images/PoliceStationImages/{fileName}";
                }

                // Set audit fields
                sdpo.IsActive = true;
                sdpo.CreatedBy = User.Identity.Name;
                sdpo.CreatedDate = DateTime.Now;

                // Save to database
                db.PoliceStations.Add(sdpo);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(sdpo);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceStation sdpo = db.PoliceStations.Find(id);
            if (sdpo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyCircle = new SelectList(db.Circles.ToList(), "Id", "Name_En");
            return View(sdpo);
        }

        [HttpPost]
        public ActionResult Edit(PoliceStation sdpo)
        {
            if (ModelState.IsValid)
            {
                sdpo.UpdatedBy = User.Identity.Name; ;
                sdpo.UpdatedDate = DateTime.Now;
                if (sdpo.MyImage != null && sdpo.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/PoliceStationImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(sdpo.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    sdpo.MyImage.SaveAs(fullPath);
                    sdpo.ImageUrl = $"/Images/PoliceStationImages/{fileName}";
                }

                db.Entry(sdpo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyCircle = new SelectList(db.Circles.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(sdpo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceStation sdpo = db.PoliceStations.Find(id);
            if (sdpo == null)
            {
                return HttpNotFound();
            }
            db.PoliceStations.Remove(sdpo);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}