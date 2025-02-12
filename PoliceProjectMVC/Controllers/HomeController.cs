using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PoliceProjectMVC.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly PDDBContext db = new PDDBContext();
        public ActionResult DualScreen()
        {
            List<MWCriminal> banners = db.MWCriminals.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList();
            return View(banners);
        }
        public ActionResult MWCount()
        {
            int banners = db.MWCriminals.Where(x => x.IsActive).Count();
            return Json(banners, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetLanguage(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            HttpCookie cultureCookie = new HttpCookie("Culture", culture);
            cultureCookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cultureCookie);
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public List<M_Menu> MainMenu()
        {
            List<M_Menu> allMenus = db.M_Menus.Where(x => x.IsActive).ToList();
            List<M_Menu> mymenus = allMenus.Where(x => x.Menu_ParentId == 0).OrderBy(p => p.MenuOrder).ToList();
            foreach (M_Menu item in mymenus)
            {
                item.SubMenus = allMenus.Where(x => x.Menu_ParentId == item.MenuId).OrderBy(p => p.MenuOrder).ToList();
            }
            return mymenus;
        }
        public List<Admin_Menu> AdminMenu()
        {
            List<Admin_Menu> allMenus = db.Admin_Menus.Where(x => x.IsActive).ToList();
            List<Admin_Menu> parentMenus = allMenus.Where(x => x.Menu_ParentId == 0).OrderBy(p => p.MenuOrder).ToList();
            foreach (Admin_Menu parent in parentMenus)
            {
                parent.SubMenus = allMenus.Where(x => x.Menu_ParentId == parent.MenuId).OrderBy(p => p.MenuOrder).ToList();
            }
            return parentMenus;
        }
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                DistrictDetail = db.DistrictDetails.AsNoTracking().FirstOrDefault(),
                Banners = db.Banners.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                PressReleases = db.PressReleases.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                MWCriminals = db.MWCriminals.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                AcheveMents = db.IAcheveMents.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                BestEmployees = db.BestEmployees.AsNoTracking().Where(x => x.IsActive).Include(x => x.Designation).OrderBy(x => x.Priority).ToList(),
                Galleries = db.ImageGalleries.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList()
            };
            return View(viewModel);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult MessageFromSP()
        {
            return View();
        }
        public ActionResult OurTeam()
        {
            List<TblOurTeam> ourTeams = db.TblOurTeams.Where(x => x.IsActive).Include(x => x.Designation).ToList();
            return View(ourTeams);
        }
        public ActionResult SuccesionList()
        {
            List<SuccessionList> successions = db.SuccessionLists.Where(x => x.IsActive).Include(x => x.Designation).ToList();
            return View(successions);
        }
        public ActionResult PoliceStation()
        {
            List<PoliceStation> stations = db.PoliceStations.Where(x => x.IsActive).Include(x => x.Circle).ToList();
            return View(stations);
        }
        public ActionResult Complaint()
        {
            ViewBag.Police = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult Complaint(Complaint complaint)
        {
            if (complaint.MyImage != null && complaint.MyImage.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/ComplaintImages/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(complaint.MyImage.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                complaint.MyImage.SaveAs(fullPath);
                complaint.ImageUrl = $"/Images/ComplaintImages/{fileName}";
            }

            // Set audit fields
            complaint.IsActive = true;
            complaint.CreatedBy = "Web";
            complaint.CreatedDate = DateTime.Now;

            // Save to database
            db.Complaints.Add(complaint);
            db.SaveChanges();

            TempData["alert"] = "submitted successfully.";
            return RedirectToAction("Complaint");
        }
        public ActionResult RTI()
        {
            return View();
        }
        public ActionResult GPOComplaint()
        {
            ViewBag.Police = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            return View();
        }
        public ActionResult GPOFemaleComplaint()
        {
            ViewBag.Police = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            return View();
        }

        [HttpPost]
        public ActionResult GPOComplaint(GrievancePolice gpo)
        {
            if (gpo.MyImage != null && gpo.MyImage.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/GrievancePoliceImages/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(gpo.MyImage.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                gpo.MyImage.SaveAs(fullPath);
                gpo.ImageUrl = $"/Images/GrievancePoliceImages/{fileName}";
            }

            // Set audit fields
            gpo.GenderType = false;
            gpo.IsActive = true;
            gpo.CreatedBy = "Web";
            gpo.CreatedDate = DateTime.Now;

            // Save to database
            db.GrievancePolices.Add(gpo);
            db.SaveChanges();

            TempData["alert"] = "submitted successfully.";
            return RedirectToAction("GPOComplaint");
        }

        [HttpPost]
        public ActionResult GPOFemaleComplaint(GrievancePolice gpo)
        {
            if (gpo.MyImage != null && gpo.MyImage.ContentLength > 0)
            {
                string imagePath = Path.Combine(Server.MapPath("~/Images/GrievancePoliceImages/"));
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(gpo.MyImage.FileName)}";
                string fullPath = Path.Combine(imagePath, fileName);

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                gpo.MyImage.SaveAs(fullPath);
                gpo.ImageUrl = $"/Images/GrievancePoliceImages/{fileName}";
            }

            // Set audit fields
            gpo.GenderType = true;
            gpo.IsActive = true;
            gpo.CreatedBy = "Web";
            gpo.CreatedDate = DateTime.Now;

            // Save to database
            db.GrievancePolices.Add(gpo);
            db.SaveChanges();

            TempData["alert"] = "submitted successfully.";
            return RedirectToAction("GPOFemaleComplaint");
        }
        public ActionResult Character()
        {
            return View();
        }
        public ActionResult Passport()
        {
            ViewBag.Police = new SelectList(db.PoliceStations.ToList(), "Id", "Name_En");
            return View();
        }
        [HttpPost]
        public ActionResult Passport(Passport passport)
        {
            
            // Set audit fields
            passport.IsActive = true;
            passport.CreatedBy = "Web";
            passport.CreatedDate = DateTime.Now;

            // Save to database
            db.Passports.Add(passport);
            db.SaveChanges();

            TempData["alert"] = "submitted successfully.";
            return RedirectToAction("Passport");
        }
        public ActionResult MissingPerson()
        {
            List<MPerson> mpersons = db.MPersons.Where(x => x.IsActive).ToList();
            return View(mpersons);
        }
        public ActionResult FoundPerson()
        {
            List<FPerson> fpersons = db.FPersons.Where(x => x.IsActive).ToList();
            return View(fpersons);
        }
        public ActionResult DeadPerson()
        {
            List<DPerson> dpersons = db.DPersons.Where(x => x.IsActive).ToList();
            return View(dpersons);
        }
        public ActionResult FeedBack()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FeedBack(Feedback feedback)
        {

            // Set audit fields
            feedback.IsActive = true;
            feedback.CreatedBy = "Web";
            feedback.CreatedDate = DateTime.Now;

            // Save to database
            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            TempData["alert"] = "submitted successfully.";
            return RedirectToAction("Passport");
        }
        public ActionResult HelpLineNo()
        {
            List<TblHelplineNumber> helplineNumbers = db.TblHelplineNumbers.Where(x => x.IsActive).Include(x => x.Designation).ToList();
            return View(helplineNumbers);
        }
        public ActionResult Annoucement()
        {
            List<Announcement> announcements = db.Announcements.Where(x => x.IsActive).ToList();
            return View(announcements);
        }
        public ActionResult Criminal()
        {
            List<CriminalList> criminals = db.CriminalLists.Where(x => x.IsActive).ToList();
            return View(criminals);
        }
        public ActionResult PeaceCommity()
        {
            List<PeaceCommittee> peaces = db.PeaceCommittees.Where(x => x.IsActive).ToList();
            return View(peaces);
        }
        public ActionResult NewsAndEvents()
        {
            List<NewsAndEvent> datalist = db.NewsAndEvents.Where(x => x.IsActive).ToList();
            return View(datalist);
        }
        public ActionResult PhotoGallery()
        {
            List<ImageGallery> datalist = db.ImageGalleries.Where(x => x.IsActive).ToList();
            return View(datalist);
        }
        public ActionResult PressRelease()
        {
            List<PressRelease> datalist = db.PressReleases.Where(x => x.IsActive).ToList();
            return View(datalist);
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult MyMainMenu()
        {
            return PartialView("_MainMenu", MainMenu());
        }
        public ActionResult AdminMainMenu()
        {
            return PartialView("_AdminMainMenu", AdminMenu());
        }
        public ActionResult Login(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                ViewBag.ReturnUrl = ReturnUrl;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(TblLogin log, string ReturnUrl)
        {
            TblLogin User = db.TblLogins.AsNoTracking().Where(x => x.Email == log.Email && x.Password == log.Password).Include(x => x.Role).FirstOrDefault();
            if (User != null)
            {
                if (User.Role.RoleName == "CoOrdinator" || User.Role.RoleName == "Admin" || User.Role.RoleName == "Staff")
                {
                    FormsAuthentication.SetAuthCookie(log.Email, false);
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Dashboard", "MyUser");
                }
            }
            TempData["response"] = "Incorrect username or password.";
            return View(log);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}