using PoliceProjectMVC.Custome_Helpers;
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
    public class FPersonController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //FPerson Crud Operation
        public ActionResult Index()
        {
            List<FPerson> fpersons = db.FPersons.ToList();
            return View(fpersons);
        }

        public ActionResult Create()
        {
            ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            ViewBag.MyGender = MyDropdownsValue.GetGender();
            return View();
        }

        [HttpPost]
        public ActionResult Create(FPerson fperson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
                ViewBag.MyGender = MyDropdownsValue.GetGender();
                TempData["responseError"] = "Data validation failed.";
                return View(fperson);
            }

            try
            {
                // Handle file upload if present
                if (fperson.MyImage != null && fperson.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/FPersonImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(fperson.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    fperson.MyImage.SaveAs(fullPath);
                    fperson.ImageUrl = $"/Images/FPersonImages/{fileName}";
                }

                // Set audit fields
                fperson.IsActive = true;
                fperson.CreatedBy = User.Identity.Name;
                fperson.CreatedDate = DateTime.Now;

                // Save to database
                db.FPersons.Add(fperson);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(fperson);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FPerson fperson = db.FPersons.Find(id);
            if (fperson == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            ViewBag.MyGender = MyDropdownsValue.GetGender();
            return View(fperson);
        }

        [HttpPost]
        public ActionResult Edit(FPerson fperson)
        {
            if (ModelState.IsValid)
            {
                fperson.UpdatedBy = User.Identity.Name; ;
                fperson.UpdatedDate = DateTime.Now;
                if (fperson.MyImage != null && fperson.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/FPersonImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(fperson.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    fperson.MyImage.SaveAs(fullPath);
                    fperson.ImageUrl = $"/Images/FPersonImages/{fileName}";
                }

                db.Entry(fperson).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyStation = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            ViewBag.MyGender = MyDropdownsValue.GetGender();
            TempData["responseError"] = "Data Error.";
            return View(fperson);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FPerson fperson = db.FPersons.Find(id);
            if (fperson == null)
            {
                return HttpNotFound();
            }
            db.FPersons.Remove(fperson);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}