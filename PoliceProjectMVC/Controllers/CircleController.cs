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
    public class CircleController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Circle Crud Operation
        public ActionResult Index()
        {
            List<Circle> circles = db.Circles.Include(x => x.Designation).ToList();
            return View(circles);
        }
        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Circle circle)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
                ViewBag.MySubDiv = new SelectList(db.SubDivisions.ToList(), "Id", "Name_En");
                TempData["responseError"] = "Data validation failed.";
                return View(circle);
            }

            try
            {
                // Handle file upload if present
                if (circle.MyImage != null && circle.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/CircleImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(circle.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    circle.MyImage.SaveAs(fullPath);
                    circle.ImageUrl = $"/Images/CircleImages/{fileName}";
                }

                // Set audit fields
                //circle.CreatedBy = User.Identity.Name;
                circle.IsActive = true;
                circle.CreatedBy = User.Identity.Name;
                circle.CreatedDate = DateTime.Now;

                // Save to database
                db.Circles.Add(circle);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(circle);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Circle circle = db.Circles.Find(id);
            if (circle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.ToList(), "Id", "Name_En");
            return View(circle);
        }

        [HttpPost]
        public ActionResult Edit(Circle circle)
        {
            if (ModelState.IsValid)
            {
                circle.UpdatedBy = User.Identity.Name;;
                circle.UpdatedDate = DateTime.Now;
                if (circle.MyImage != null && circle.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/CircleImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(circle.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    circle.MyImage.SaveAs(fullPath);
                    circle.ImageUrl = $"/Images/CircleImages/{fileName}";
                }

                db.Entry(circle).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(circle);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Circle circle = db.Circles.Find(id);
            if (circle == null)
            {
                return HttpNotFound();
            }
            db.Circles.Remove(circle);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}