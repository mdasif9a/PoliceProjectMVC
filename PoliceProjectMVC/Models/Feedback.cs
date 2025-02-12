using System;
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
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string District { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}