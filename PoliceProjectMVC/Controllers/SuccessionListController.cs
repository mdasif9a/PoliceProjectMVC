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
    public class SuccessionListController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //SuccessionList Crud Operation
        public ActionResult Index()
        {
            List<SuccessionList> successionLists = db.SuccessionLists.Include(x => x.Designation).ToList();
            return View(successionLists);
        }
        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SuccessionList successionList)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
                TempData["responseError"] = "Data validation failed.";
                return View(successionList);
            }

            try
            {
                // Handle file upload if present
                if (successionList.MyImage != null && successionList.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SuccessionListImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(successionList.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    successionList.MyImage.SaveAs(fullPath);
                    successionList.ImageUrl = $"/Images/SuccessionListImages/{fileName}";
                }

                // Set audit fields
                successionList.IsActive = true;
                successionList.CreatedBy = User.Identity.Name;
                successionList.CreatedDate = DateTime.Now;

                // Save to database
                db.SuccessionLists.Add(successionList);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(successionList);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuccessionList successionList = db.SuccessionLists.Find(id);
            if (successionList == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            return View(successionList);
        }

        [HttpPost]
        public ActionResult Edit(SuccessionList successionList)
        {
            if (ModelState.IsValid)
            {
                successionList.UpdatedBy = User.Identity.Name; ;
                successionList.UpdatedDate = DateTime.Now;
                if (successionList.MyImage != null && successionList.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SuccessionListImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(successionList.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    successionList.MyImage.SaveAs(fullPath);
                    successionList.ImageUrl = $"/Images/SuccessionListImages/{fileName}";
                }

                db.Entry(successionList).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.Designations.ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data Error.";
            return View(successionList);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuccessionList successionList = db.SuccessionLists.Find(id);
            if (successionList == null)
            {
                return HttpNotFound();
            }
            db.SuccessionLists.Remove(successionList);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }

    }
}