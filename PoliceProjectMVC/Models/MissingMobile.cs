using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class MissingMobile
    {
        public int Id { get; set; }
        [Required]
        public string IMEINumber { get; set; }
        [Required]
        public string MobileNumber { get; set; }

        public string AlternateNumber { get; set; }
        [Required]
        public DateTime DateOfMissing { get; set; }
        [Required]
        public string AadharNumber { get; set; }
        public string DocumentPath { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }

}