﻿using PoliceProjectMVC.Custome_Helpers;
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
    public class DPersonController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //DPerson Crud Operation
        public ActionResult Index()
        {
            List<DPerson> dpersons = db.DPersons.ToList();
            return View(dpersons);
        }

        public ActionResult Create()
        {
            ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            ViewBag.MyGender = MyDropdownsValue.GetGender();
            return View();
        }

        [HttpPost]
        public ActionResult Create(DPerson dperson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
                ViewBag.MyGender = MyDropdownsValue.GetGender();
                TempData["responseError"] = "Data validation failed.";
                return View(dperson);
            }

            try
            {
                // Handle file upload if present
                if (dperson.MyImage != null && dperson.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/DPersonImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(dperson.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    dperson.MyImage.SaveAs(fullPath);
                    dperson.ImageUrl = $"/Images/DPersonImages/{fileName}";
                }

                // Set audit fields
                dperson.IsActive = true;
                dperson.CreatedBy = User.Identity.Name;
                dperson.CreatedDate = DateTime.Now;

                // Save to database
                db.DPersons.Add(dperson);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(dperson);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DPerson dperson = db.DPersons.Find(id);
            if (dperson == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            ViewBag.MyGender = MyDropdownsValue.GetGender();
            return View(dperson);
        }

        [HttpPost]
        public ActionResult Edit(DPerson dperson)
        {
            if (ModelState.IsValid)
            {
                dperson.UpdatedBy = User.Identity.Name; ;
                dperson.UpdatedDate = DateTime.Now;
                if (dperson.MyImage != null && dperson.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/DPersonImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(dperson.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    dperson.MyImage.SaveAs(fullPath);
                    dperson.ImageUrl = $"/Images/DPersonImages/{fileName}";
                }

                db.Entry(dperson).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            ViewBag.MyGender = MyDropdownsValue.GetGender();
            TempData["responseError"] = "Data Error.";
            return View(dperson);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DPerson dperson = db.DPersons.Find(id);
            if (dperson == null)
            {
                return HttpNotFound();
            }
            db.DPersons.Remove(dperson);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}