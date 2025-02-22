using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PoliceProjectMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            HttpCookie cultureCookie = Request.Cookies["Culture"];
            string culture = cultureCookie != null ? cultureCookie.Value : "en-IN";

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Task.Run(() => StartScheduler());
        }

        private async Task StartScheduler()
        {
            while (true)
            {
                try
                {
                    var scheduler = new MessageSchedulerService();
                    scheduler.CheckAndSendScheduledMessages();
                    System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/log.txt"), "Task Excuted on : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/log.txt"), ex.ToString());
                }

                // Wait for 24 hours before running again
                await Task.Delay(TimeSpan.FromHours(24));
            }
        }
    }
}
