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
    public class PostingListController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //PostingList Crud Operation
        public ActionResult Index()
        {
            List<PostingList> postings = db.PostingLists.ToList();
            return View(postings);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostingList posting)
        {
            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                return View(posting);
            }

            try
            {
                // Handle file upload if present
                if (posting.MyImage != null && posting.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/PostingListImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(posting.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    posting.MyImage.SaveAs(fullPath);
                    posting.ImageUrl = $"/Images/PostingListImages/{fileName}";
                }

                // Set audit fields
                posting.IsActive = true;
                posting.CreatedBy = User.Identity.Name;
                posting.CreatedDate = DateTime.Now;

                // Save to database
                db.PostingLists.Add(posting);
                db.SaveChanges();

                TempData["response"] = "created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                return View(posting);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostingList posting = db.PostingLists.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            return View(posting);
        }

        [HttpPost]
        public ActionResult Edit(PostingList posting)
        {
            if (ModelState.IsValid)
            {
                posting.UpdatedBy = User.Identity.Name; ;
                posting.UpdatedDate = DateTime.Now;
                if (posting.MyImage != null && posting.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/PostingListImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(posting.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    posting.MyImage.SaveAs(fullPath);
                    posting.ImageUrl = $"/Images/PostingListImages/{fileName}";
                }

                db.Entry(posting).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            TempData["responseError"] = "Data Error.";
            return View(posting);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostingList posting = db.PostingLists.Find(id);
            if (posting == null)
            {
                return HttpNotFound();
            }
            db.PostingLists.Remove(posting);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}