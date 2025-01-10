using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Passport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter File Number")]
        public string FileNumber { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name")]
        public string ApplicantName { get; set; }

        [Required(ErrorMessage = "Enter Passport Type")]
        public string PassportType { get; set; }

        [Required(ErrorMessage = "Enter Mobile No.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Select Police Station")]
        public int PoliceStationId { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Select District")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Enter Comments")]
        public string Comments { get; set; }

        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}