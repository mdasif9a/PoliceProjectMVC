﻿using PoliceProjectMVC.Custome_Helpers;
using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Controllers
{
    public class LinkController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //Link Crud Operation
        public ActionResult Index()
        {
            List<Link> links = db.Links.ToList();
            return View(links);
        }
        public ActionResult Create()
        {
            ViewBag.LinkType = MyDropdownsValue.GetLinkType();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Link link)
        {
            if (ModelState.IsValid)
            {
                link.IsActive = true;
                link.CreatedDate = DateTime.Now;
                link.CreatedBy = User.Identity.Name;
                db.Links.Add(link);
                db.SaveChanges();
                TempData["response"] = "Created Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.LinkType = MyDropdownsValue.GetLinkType();
            TempData["responseError"] = "Data Error.";
            return View(link);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.LinkType = MyDropdownsValue.GetLinkType();
            return View(link);
        }

        [HttpPost]
        public ActionResult Edit(Link link)
        {
            if (ModelState.IsValid)
            {

                link.UpdatedDate = DateTime.Now;
                link.UpdatedBy = User.Identity.Name; ;
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.LinkType = MyDropdownsValue.GetLinkType();
            TempData["responseError"] = "Data Error.";
            return View(link);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            db.Links.Remove(link);
            db.SaveChanges();
            TempData["response"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}