using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Custome_Helpers
{
    public static class MyDropdownsValue
    {
        public static SelectList GetDistrictForSelectList()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "West  Champaran", Value = "West  Champaran" }
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