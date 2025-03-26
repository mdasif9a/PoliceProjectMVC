using System;
using PoliceProjectMVC.Custome_Helpers;
using PoliceProjectMVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace PoliceProjectMVC.Controllers
{

    public class SRNSRController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();

        //json data
        public JsonResult GetSMHead(int id)
        {
            var sMHeads = db.SMHeads.AsNoTracking().Where(x => x.MHeadId == id).Select(x => new
            {
                x.Id,
                x.Name_EN
            }).OrderBy(x => x.Name_EN).ToList();
            return Json(sMHeads, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCircles(int id)
        {
            var circles = db.Circles.AsNoTracking().Where(x => x.SubDivId == id).Select(x => new
            {
                x.Id,
                x.Name_En
            }).OrderBy(x => x.Name_En).ToList();
            return Json(circles, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStations(int id)
        {
            var policeStations = db.PoliceStations.AsNoTracking().Where(x => x.CircleId == id).Select(x => new
            {
                x.Id,
                x.Name_En
            }).OrderBy(x => x.Name_En).ToList();
            return Json(policeStations, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModusOpernadi(int id)
        {
            var modusoperandi = db.MOperandis.AsNoTracking().Where(x => x.SMHeadId == id).Select(x => new
            {
                x.Id,
                x.Name_EN
            }).OrderBy(x => x.Name_EN).ToList();
            return Json(modusoperandi, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAccused(Accused accused)
        {
            if (accused.ConfessionFile != null && accused.ConfessionFile.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/ConFession/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(accused.ConfessionFile.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                accused.ConfessionFile.SaveAs(fullPath);
                accused.ConfessionStatement = $"/Images/SRNSRImages/ConFession/{fileName}";
            }

            if (accused.AccusedFile != null && accused.AccusedFile.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/AccusedFile/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(accused.AccusedFile.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                accused.AccusedFile.SaveAs(fullPath);
                accused.ImageOfAccused = $"/Images/SRNSRImages/AccusedFile/{fileName}";
            }

            accused.IsActive = true;
            accused.CreatedBy = User.Identity.Name;
            accused.CreatedDate = DateTime.Now;

            db.Accuseds.Add(accused);
            db.SaveChanges();
            return Json(accused.Id);
        }
        public ActionResult Index()
        {
            List<SRNSRCase> cases = db.SRNSRCases.AsNoTracking()
                .Include(x => x.PoliceStation)
                .Include(x => x.MHead)
                .OrderByDescending(x => x.CreatedDate).ToList();
            return View(cases);
        }

        public ActionResult GetAccuseds(int caseId)
        {
            var accuseds = db.Accuseds.Where(p => p.SRNSRCaseId == caseId).ToList();
            return PartialView("_AccusedsViewPartial", accuseds);
        }

        public ActionResult Create()
        {
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            SRNSRCase sRNSR = new SRNSRCase
            {
                NoOfKnownAccused = 1,
                NoOfUnknownAccused = 0,
                ChargesheetYesNo = "No",
                JailDate = DateTime.Today,
                CaseDate = DateTime.Today,
                DateAndTimeOfOccurance = DateTime.Now
            };
            return View(sRNSR);
        }

        [HttpPost]
        public ActionResult Create(SRNSRCase model)
        {
            int cmsdays = db.MHeads.Find(model.MHeadId).CSM_Days;


            if (!ModelState.IsValid)
            {
                TempData["responseError"] = "Data validation failed.";
                ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
                ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                return View(model);
            }

            try
            {

                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.CaseDocument = $"/Images/SRNSRImages/{fileName}";
                }

                if (model.ConfessionFile != null && model.ConfessionFile.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/ConFession/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.ConfessionFile.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.ConfessionFile.SaveAs(fullPath);
                    model.ConfessionStatement = $"/Images/SRNSRImages/ConFession/{fileName}";
                }

                if (model.AccusedFile != null && model.AccusedFile.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/AccusedFile/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.AccusedFile.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.AccusedFile.SaveAs(fullPath);
                    model.ImageOfAccused = $"/Images/SRNSRImages/AccusedFile/{fileName}";
                }

                model.IsActive = true;
                model.CreatedBy = User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.SecondWPMessageSent = false;
                model.ThirdWPMessageSent = false;
                model.FourthWPMessageSent = false;
                model.LastChargeSheetdate = DateTime.Today.AddDays(cmsdays);
                model.Status = "Pending";

                db.SRNSRCases.Add(model);
                db.SaveChanges();
                if (model.CaseType == "SR")
                {
                    int sdpo = db.SubDivisions.Where(x => x.Id == model.SubDivisionId).Select(x => x.SdpoId).FirstOrDefault();
                    string spnumber = db.DistrictDetails.Select(x => x.ContactNo).FirstOrDefault();
                    string sdponumber = db.SDPOs.Where(x => x.Id == sdpo).Select(x => x.MobileNo).FirstOrDefault();
                    var policestation = db.PoliceStations.Where(x => x.Id == model.PoliceStationId).FirstOrDefault();
                    string shonumber = policestation.MobileNo;
                    var validNumbers = new List<string> { spnumber, sdponumber, shonumber, model.IoMobile, model.CIMobile }
                        .Where(num => !string.IsNullOrEmpty(num));

                    int leftdays = (model.LastChargeSheetdate.Date - DateTime.Today).Days;

                    string message = $"Section : {model.Section} Case-No : {model.SrNo} \nPolice Station : {policestation.Name_En}\nAccused Name : {model.AccusedName}\nAddress : {model.AccusedAddress}\nDate of Arrest : {model.JailDate.ToShortDateString()}\nLast Date of ChargeSheet : {model.LastChargeSheetdate.ToShortDateString()}\nDays Left : {leftdays} days";

                    foreach (var item in validNumbers)
                    {
                        SendWPMessage(item, message);
                    }

                    //SendWPMessage("9110036432", "Demo message");
                }
                TempData["response"] = "Created successfully and message sent.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["responseError"] = $"An error occurred: {ex.Message}";
                ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
                ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
                return View(model);
            }
        }

        public ActionResult TaskMessage()
        {
            var scheduler = new MessageSchedulerService();
            scheduler.CheckAndSendScheduledMessages();
            return Content("Scheduler Executed Done");
        }

        private void SendWPMessage(string number, string message)
        {
            ApiAndWebContent apidetails = db.ApiAndWebContents.FirstOrDefault();
            if (string.IsNullOrEmpty(number))
            {
                TempData["responseError"] = $"Numbers Not Found to sent message.";
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, apidetails.ApiUrl);
                    request.Headers.Add("Authorization", apidetails.ApiHeader);

                    var jsonPayload = new
                    {
                        messaging_product = "whatsapp",
                        recipient_type = "individual",
                        to = number,
                        type = "text",
                        text = new { preview_url = false, body = message }
                    };

                    string jsonContent = JsonSerializer.Serialize(jsonPayload);
                    request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var response = client.SendAsync(request).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine(responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending WhatsApp message: {ex.Message}");
            }
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SRNSRCase model = db.SRNSRCases.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SRNSRCase model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = User.Identity.Name;
                model.UpdatedDate = DateTime.Now;


                if (model.MyImage != null && model.MyImage.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.MyImage.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.MyImage.SaveAs(fullPath);
                    model.CaseDocument = $"/Images/ActImages/{fileName}";
                }

                if (model.ConfessionFile != null && model.ConfessionFile.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/ConFession/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.ConfessionFile.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.ConfessionFile.SaveAs(fullPath);
                    model.ConfessionStatement = $"/Images/SRNSRImages/ConFession/{fileName}";
                }

                if (model.AccusedFile != null && model.AccusedFile.ContentLength > 0)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Images/SRNSRImages/AccusedFile/"));
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.AccusedFile.FileName)}";
                    string fullPath = Path.Combine(imagePath, fileName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    model.AccusedFile.SaveAs(fullPath);
                    model.ImageOfAccused = $"/Images/SRNSRImages/AccusedFile/{fileName}";
                }

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["response"] = "Updated successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.MyMHead = new SelectList(db.MHeads.Select(x => new { x.Id, x.Name_EN }).OrderBy(x => x.Name_EN).ToList(), "Id", "Name_EN");
            ViewBag.MySubDiv = new SelectList(db.SubDivisions.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            ViewBag.MyDesigNation = new SelectList(db.Designations.Select(x => new { x.Id, x.Name_En }).OrderBy(x => x.Name_En).ToList(), "Id", "Name_En");
            TempData["responseError"] = "Data validation failed.";
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Act model = db.Acts.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Acts.Remove(model);
            db.SaveChanges();
            TempData["response"] = "Deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}