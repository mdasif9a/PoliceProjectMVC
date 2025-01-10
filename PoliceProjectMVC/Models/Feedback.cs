﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name")]
        public string ApplicantName { get; set; }

        [Required(ErrorMessage = "Enter Mobile No.")]
        public string MobileNo { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Select District")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Enter Comments")]
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}