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
    public class BestEmployeeController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //BestEmployee Crud Operation
        public ActionResult Index()
        {
            List<BestEmployee> bestemployees = db.BestEmployees.ToList();
            return View(bestemployees);
        }
        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(BestEmployee bestemployee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
                TempData["responseError"] = "Data validation failed.";
                return View(bestemployee);
            }

            try
            {
                // Handle file upload if present
                if (bestemployee.MyImage != null && bestemployee.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/BestEmployeeImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(bestemployee.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    bestemployee.MyImage.SaveAs(fullPath);
                    bestemployee.ImageUrl = $"/Images/BestEmployeeImages/{fileName}";
                }

                // Set audit fields
                //bestemployee.CreatedBy = "admin";
                bestemployee.IsActive = true;
                bestemployee.CreatedBy = "admin";
                bestemployee.CreatedDate = DateTime.Now;

                // Save to database
                db.BestEmployees.Add(bestemployee);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(bestemployee);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BestEmployee bestemployee = db.BestEmployees.Find(id);
            if (bestemployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View(bestemployee);
        }

        [HttpPost]
        public ActionResult Edit(BestEmployee bestemployee)
        {
            if (ModelState.IsValid)
            {
                bestemployee.UpdatedBy = "admin";
                bestemployee.UpdatedDate = DateTime.Now;
                if (bestemployee.MyImage != null && bestemployee.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/BestEmployeeImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(bestemployee.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    bestemployee.MyImage.SaveAs(fullPath);
                    bestemployee.ImageUrl = $"/Images/BestEmployeeImages/{fileName}";
                }

                db.Entry(bestemployee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            TempData["responseError"] = "Data Error.";
            return View(bestemployee);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BestEmployee bestemployee = db.BestEmployees.Find(id);
            if (bestemployee == null)
            {
                return HttpNotFound();
            }
            db.BestEmployees.Remove(bestemployee);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }

    }
}