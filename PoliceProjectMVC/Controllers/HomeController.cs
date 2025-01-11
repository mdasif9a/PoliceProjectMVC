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


        // GET: Home
        public ActionResult Index()
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
            TblLogin User = db.TblLogins.Where(x => x.Email == log.Email && x.Password == log.Password).FirstOrDefault();
            if (User != null)
            {
                //if (User.Role == "CoOrdinator" || User.Role == "Admin" || User.Role == "Staff")
                //{
                FormsAuthentication.SetAuthCookie(log.Email, false);
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Dashboard");
                //}
            }
            TempData["response"] = "Incorrect username or password.";
            return View(log);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Dashboard()
        {
            ViewBag.TotalComplaints = 0;
            ViewBag.PendingComplaints = 0;
            ViewBag.ClosedComplaints = 0;
            ViewBag.RejectedComplaints = 0;

            ViewBag.TotalCharacterCertificates = 0;
            ViewBag.TotalMissingPersons = 0;
            ViewBag.TotalFoundPersons = 0;
            ViewBag.TotalPassports = 0;

            ViewBag.TotalFeedbackSuggestions = 0;

            return View("AdminDash");
        }
    }
}