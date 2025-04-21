using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceProjectMVC.Custome_Helpers
{
    public static class MyDropdownsValue
    {
        public static SelectList EmptySelectList()
        {
            return new SelectList(Enumerable.Empty<object>(), "Value", "Text");
        }

        public static SelectList GetYesNo()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Yes", Value = "Yes" },
                                new SelectListItem { Text = "No", Value = "No" }
                            }, "Value", "Text");
        }
        public static SelectList CaseTypeSRNSR()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "SR", Value = "SR" },
                                new SelectListItem { Text = "NSR", Value = "NSR" }
                            }, "Value", "Text");
        }

        public static SelectList GetDistrict()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "West Champaran", Value = "West Champaran" }
                            }, "Value", "Text");
        }

        public static SelectList GetDistrictWithOther()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "West Champaran", Value = "West Champaran" },
                                new SelectListItem { Text = "Others", Value = "Others" }
                            }, "Value", "Text");
        }

        public static SelectList GetGender()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Male", Value = "Male" },
                                new SelectListItem { Text = "Female", Value = "Female" },
                                new SelectListItem { Text = "Others", Value = "Others" }
                            }, "Value", "Text");
        }

        public static SelectList GetPassportType()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Passport", Value = "Passport" },
                                new SelectListItem { Text = "Police Clearance Certificate", Value = "Police Clearance Certificate" }
                            }, "Value", "Text");
        }

        public static SelectList GetHelplineType()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Officer", Value = "Officer" },
                                new SelectListItem { Text = "Other Department", Value = "Other Department" }
                            }, "Value", "Text");
        }

        public static SelectList GetLinkType()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "MainPage", Value = "MainPage" },
                                new SelectListItem { Text = "Sidebar", Value = "Sidebar" }
                            }, "Value", "Text");
        }

        public static SelectList GetComplainType()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Case Related", Value = "Case Related" },
                                new SelectListItem { Text = "Other Related", Value = "Other Related" }
                            }, "Value", "Text");
        }

        public static SelectList GetEmployeeStatusType()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Retired", Value = "Retired" },
                                new SelectListItem { Text = "Working Official", Value = "Working Official" }
                            }, "Value", "Text");
        }

        public static SelectList GetCaseStatus()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Pending", Value = "Pending" },
                                new SelectListItem { Text = "Rejected", Value = "Rejected" },
                                new SelectListItem { Text = "Closed", Value = "Closed" }
                            }, "Value", "Text");
        }

        public static SelectList GetDocType()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Pdf", Value = "Pdf" },
                                new SelectListItem { Text = "Image", Value = "Image" },
                                new SelectListItem { Text = "Others", Value = "Others" }
                            }, "Value", "Text");
        }

        public static SelectList GetMobileStatus()
        {
            return new SelectList(new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Pending", Value = "Pending" },
                                new SelectListItem { Text = "Approved", Value = "Approved" },
                                new SelectListItem { Text = "Rejected", Value = "Rejected" },
                                new SelectListItem { Text = "Closed", Value = "Closed" }
                            }, "Value", "Text");
        }
    }
}