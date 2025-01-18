using PoliceProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
                DistrictDetail = db.DistrictDetails.FirstOrDefault(),
                Banners = db.Banners.Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                PressReleases = db.PressReleases.Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                MWCriminals = db.MWCriminals.Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                AcheveMents = db.IAcheveMents.Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                BestEmployees = db.BestEmployees.Where(x => x.IsActive).OrderBy(x => x.Priority).ToList(),
                Galleries = db.ImageGalleries.Where(x => x.IsActive).OrderBy(x => x.Priority).ToList()
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
            return View();
        }
        public ActionResult SuccesionList()
        {
            return View();
        }
        public ActionResult PoliceStation()
        {
            return View();
        }
        public ActionResult Complaint()
        {
            return View();
        }
        public ActionResult GPOComplaint()
        {
            return View();
        }
        public ActionResult GPOFemaleComplaint()
        {
            return View();
        }
        public ActionResult Character()
        {
            return View();
        }
        public ActionResult Passport()
        {
            return View();
        }
        public ActionResult MissingPerson()
        {
            return View();
        }
        public ActionResult FoundPerson()
        {
            return View();
        }
        public ActionResult DeadPerson()
        {
            return View();
        }
        public ActionResult FeedBack()
        {
            return View();
        }
        public ActionResult HelpLineNo()
        {
            return View();
        }
        public ActionResult Annoucement()
        {
            return View();
        }
        public ActionResult Criminal()
        {
            return View();
        }
        public ActionResult PeaceCommity()
        {
            return View();
        }
        public ActionResult NewsAndEvents()
        {
            return View();
        }
        public ActionResult PhotoGallery()
        {
            return View();
        }
        public ActionResult PressRelease()
        {
            return View();
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