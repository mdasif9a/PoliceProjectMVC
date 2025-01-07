using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Custome_Helpers
{
    public static class MyDropdownsValue
    {
        public static SelectList GetFeesForSelectList()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "1st Year", Value = "1st" },
                                new SelectListItem { Text = "2nd Year", Value = "2nd" },
                                new SelectListItem { Text = "3rd Year", Value = "3rd" },
                                new SelectListItem { Text = "4th Year", Value = "4th" }
                            }, "Value", "Text");
        }

        public static SelectList GetModeForSelectList()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Cash", Value = "Cash" },
                                new SelectListItem { Text = "Online", Value = "Online" }
                            }, "Value", "Text");
        }
    }
}