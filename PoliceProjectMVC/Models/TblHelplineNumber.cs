using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class TblHelplineNumber
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name in Hindi")]
        public string Name_Hi { get; set; }

        [Required(ErrorMessage = "Enter Name in English")]
        public string Name_En { get; set; }

        [Required(ErrorMessage = "Select Designation")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Select Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Enter Helpline Numbers")]
        public string HelplineNumbers { get; set; }

        [Required(ErrorMessage = "Enter Discription in Hindi")]
        public string Description_Hi { get; set; }

        [Required(ErrorMessage = "Enter Discription in English")]
        public string Description_En { get; set; }

        [Required(ErrorMessage = "Enter Priority")]
        public int Priority { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public Designation Designation { get; set; }
    }
}