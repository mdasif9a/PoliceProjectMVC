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
    public class IAcheveMentController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //IAcheveMent Crud Operation
        public ActionResult Index()
        {
            List<IAcheveMent> iacheveMents = db.IAcheveMents.ToList();
            return View(iacheveMents);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IAcheveMent iachevement)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(iachevement);
            }

            try
            {
                // Handle file upload if present
                if (iachevement.MyImage != null && iachevement.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/IAcheveMentImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(iachevement.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    iachevement.MyImage.SaveAs(fullPath);
                    iachevement.ImageUrl = $"/Images/IAcheveMentImages/{fileName}";
                }

                // Set audit fields
                //iachevement.CreatedBy = "admin";
                iachevement.CreatedBy = "admin";
                iachevement.CreatedDate = DateTime.Now;

                // Save to database
                db.IAcheveMents.Add(iachevement);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(iachevement);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IAcheveMent iachevement = db.IAcheveMents.Find(id);
            if (iachevement == null)
            {
                return HttpNotFound();
            }
            return View(iachevement);
        }

        [HttpPost]
        public ActionResult Edit(IAcheveMent iachevement)
        {
            if (ModelState.IsValid)
            {
                iachevement.UpdatedBy = "admin";
                iachevement.UpdatedDate = DateTime.Now;
                if (iachevement.MyImage != null && iachevement.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/IAcheveMentImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(iachevement.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    iachevement.MyImage.SaveAs(fullPath);
                    iachevement.ImageUrl = $"/Images/IAcheveMentImages/{fileName}";
                }

                db.Entry(iachevement).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(iachevement);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IAcheveMent iachevement = db.IAcheveMents.Find(id);
            if (iachevement == null)
            {
                return HttpNotFound();
            }
            db.IAcheveMents.Remove(iachevement);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }

    }
}