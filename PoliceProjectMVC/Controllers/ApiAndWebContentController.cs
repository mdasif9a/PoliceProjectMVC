using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Controllers
{
    public class ApiAndWebContentController : Controller
    {
        // GET: ApiAndWebContent
        private readonly PDDBContext db = new PDDBContext();
        public ActionResult WebContent()
        {
            ApiAndWebContent webContent = db.ApiAndWebContents.FirstOrDefault();
            if (webContent == null)
            {
                webContent = new ApiAndWebContent();
            }
            return View(webContent);
        }

        [HttpPost]
        public ActionResult Edit(ApiAndWebContent apiAndWebContent)
        {
            if (ModelState.IsValid)
            {
                if (apiAndWebContent.Id == 0)
                {
                    db.ApiAndWebContents.Add(apiAndWebContent);
                }
                else
                {
                    db.Entry(apiAndWebContent).State = EntityState.Modified;
                }
                db.SaveChanges();
                TempData["response"] = "Updated Successfully.";
                return RedirectToAction("WebContent");
            }
            TempData["responseError"] = "Data Error.";
            return View(apiAndWebContent);
        }
    }
}