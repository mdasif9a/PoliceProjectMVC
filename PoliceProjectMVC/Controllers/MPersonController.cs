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
    public class MPersonController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //MPerson Crud Operation
        public ActionResult Index()
        {
            List<MPerson> mpersons = db.MPersons.ToList();
            return View(mpersons);
        }

        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDSP = new SelectList(db.DSPs.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(MPerson mperson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
                ViewBag.MyDSP = new SelectList(db.DSPs.ToList(), "Id", "Name_En");
                TempData["responseError"] = "Data validation failed.";
                return View(mperson);
            }

            try
            {
                // Handle file upload if present
                if (mperson.MyImage != null && mperson.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/MPersonImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(mperson.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    mperson.MyImage.SaveAs(fullPath);
                    mperson.ImageUrl = $"/Images/MPersonImages/{fileName}";
                }

                // Set audit fields
                mperson.IsActive = true;
                mperson.CreatedBy = User.Identity.Name;
                mperson.CreatedDate = DateTime.Now;

                // Save to database
                db.MPersons.Add(mperson);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(mperson);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPerson mperson = db.MPersons.Find(id);
            if (mperson == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDSP = new SelectList(db.DSPs.ToList(), "Id", "Name_En");
            return View(mperson);
        }

        [HttpPost]
        public ActionResult Edit(MPerson mperson)
        {
            if (ModelState.IsValid)
            {
                mperson.UpdatedBy = User.Identity.Name; ;
                mperson.UpdatedDate = DateTime.Now;
                if (mperson.MyImage != null && mperson.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/MPersonImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(mperson.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    mperson.MyImage.SaveAs(fullPath);
                    mperson.ImageUrl = $"/Images/MPersonImages/{fileName}";
                }

                db.Entry(mperson).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            ViewBag.MyDSP = new SelectList(db.DSPs.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(mperson);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MPerson mperson = db.MPersons.Find(id);
            if (mperson == null)
            {
                return HttpNotFound();
            }
            db.MPersons.Remove(mperson);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}