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
            List<M_Menu> mymenus = db.M_Menus.Where(x => x.Menu_ParentId == 0 && x.IsActive).OrderBy(p => p.MenuOrder).ToList();
            foreach (var item in mymenus)
            {
                item.SubMenus = db.M_Menus.Where(x => x.Menu_ParentId == item.MenuId && x.IsActive).OrderBy(p => p.MenuOrder).ToList();
            }
            return mymenus;
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
            TblLogin User = db.TblLogins.Where(x => x.Username == log.Username && x.Password == log.Password).FirstOrDefault();
            if (User != null)
            {
                if (User.Role == "CoOrdinator" || User.Role == "Admin" || User.Role == "Staff")
                {
                    FormsAuthentication.SetAuthCookie(log.Username, false);
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index");
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

        public ActionResult Dashboard()
        {
            return View("AdminDash");
            //return View("CoorDash");
        }
    }
}