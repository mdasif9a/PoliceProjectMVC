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
    public class MWCriminalController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //MWCriminal Crud Operation
        public ActionResult Index()
        {
            List<MWCriminal> mWCriminals = db.MWCriminals.ToList();
            return View(mWCriminals);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MWCriminal mWCriminal)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(mWCriminal);
            }

            try
            {
                // Handle file upload if present
                if (mWCriminal.MyImage != null && mWCriminal.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/MWCriminalImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(mWCriminal.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    mWCriminal.MyImage.SaveAs(fullPath);
                    mWCriminal.ImageUrl = $"/Images/MWCriminalImages/{fileName}";
                }

                // Set audit fields
                //mWCriminal.CreatedBy = User.Identity.Name;
                mWCriminal.IsActive = true;
                mWCriminal.CreatedBy = User.Identity.Name;
                mWCriminal.CreatedDate = DateTime.Now;

                // Save to database
                db.MWCriminals.Add(mWCriminal);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(mWCriminal);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MWCriminal mWCriminal = db.MWCriminals.Find(id);
            if (mWCriminal == null)
            {
                return HttpNotFound();
            }
            return View(mWCriminal);
        }

        [HttpPost]
        public ActionResult Edit(MWCriminal mWCriminal)
        {
            if (ModelState.IsValid)
            {
                mWCriminal.UpdatedBy = User.Identity.Name;;
                mWCriminal.UpdatedDate = DateTime.Now;
                if (mWCriminal.MyImage != null && mWCriminal.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/MWCriminalImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(mWCriminal.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    mWCriminal.MyImage.SaveAs(fullPath);
                    mWCriminal.ImageUrl = $"/Images/MWCriminalImages/{fileName}";
                }

                db.Entry(mWCriminal).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(mWCriminal);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MWCriminal mWCriminal = db.MWCriminals.Find(id);
            if (mWCriminal == null)
            {
                return HttpNotFound();
            }
            db.MWCriminals.Remove(mWCriminal);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }

    }
}