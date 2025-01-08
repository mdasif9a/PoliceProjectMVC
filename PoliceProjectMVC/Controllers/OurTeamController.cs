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
    public class OurTeamController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //OurTeam Crud Operation
        public ActionResult Index()
        {
            List<TblOurTeam> ourTeams = db.TblOurTeams.ToList();
            return View(ourTeams);
        }
        public ActionResult Create()
        {
            ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblOurTeam ourteam)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
                TempData["responseError"] = "Data validation failed.";
                return View(ourteam);
            }

            try
            {
                // Handle file upload if present
                if (ourteam.MyImage != null && ourteam.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/OurTeamImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(ourteam.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    ourteam.MyImage.SaveAs(fullPath);
                    ourteam.ImageUrl = $"/Images/OurTeamImages/{fileName}";
                }

                // Set audit fields
                //ourteam.CreatedBy = "admin";
                ourteam.CreatedBy = "admin";
                ourteam.CreatedDate = DateTime.Now;

                // Save to database
                db.TblOurTeams.Add(ourteam);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(ourteam);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblOurTeam ourteam = db.TblOurTeams.Find(id);
            if (ourteam == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            return View(ourteam);
        }

        [HttpPost]
        public ActionResult Edit(TblOurTeam ourteam)
        {
            if (ModelState.IsValid)
            {
                ourteam.UpdatedBy = "admin";
                ourteam.UpdatedDate = DateTime.Now;
                if (ourteam.MyImage != null && ourteam.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/UserProfileImg/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(ourteam.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    ourteam.MyImage.SaveAs(fullPath);
                    ourteam.ImageUrl = $"/Images/UserProfileImg/{fileName}";
                }

                db.Entry(ourteam).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyDesignation = new SelectList(db.TblRoles.ToList(), "Id", "RoleName");
            TempData["responseError"] = "Data Error.";
            return View(ourteam);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblOurTeam ourteam = db.TblOurTeams.Find(id);
            if (ourteam == null)
            {
                return HttpNotFound();
            }
            db.TblOurTeams.Remove(ourteam);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }

    }
}