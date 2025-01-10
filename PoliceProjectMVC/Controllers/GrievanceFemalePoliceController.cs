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
    public class GrievanceFemalePoliceController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //GrievancePolice Crud Operation
        public ActionResult Index()
        {
            List<GrievancePolice> gpos = db.GrievancePolices.Where(x => x.GenderType).ToList();
            return View(gpos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GrievancePolice gpo)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(gpo);
            }

            try
            {
                // Handle file upload if present
                if (gpo.MyImage != null && gpo.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/GrievancePoliceImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(gpo.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    gpo.MyImage.SaveAs(fullPath);
                    gpo.ImageUrl = $"/Images/GrievancePoliceImages/{fileName}";
                }

                // Set audit fields
                gpo.GenderType = true;
                gpo.IsActive = true;
                gpo.CreatedBy = User.Identity.Name;
                gpo.CreatedDate = DateTime.Now;

                // Save to database
                db.GrievancePolices.Add(gpo);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(gpo);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrievancePolice gpo = db.GrievancePolices.Find(id);
            if (gpo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDSP = new SelectList(db.DSPs.ToList(), "Id", "Name_En");
            return View(gpo);
        }

        [HttpPost]
        public ActionResult Edit(GrievancePolice gpo)
        {
            if (ModelState.IsValid)
            {
                gpo.UpdatedBy = User.Identity.Name; ;
                gpo.UpdatedDate = DateTime.Now;
                if (gpo.MyImage != null && gpo.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/GrievancePoliceImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(gpo.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    gpo.MyImage.SaveAs(fullPath);
                    gpo.ImageUrl = $"/Images/GrievancePoliceImages/{fileName}";
                }

                db.Entry(gpo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDSP = new SelectList(db.DSPs.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(gpo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrievancePolice gpo = db.GrievancePolices.Find(id);
            if (gpo == null)
            {
                return HttpNotFound();
            }
            db.GrievancePolices.Remove(gpo);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}