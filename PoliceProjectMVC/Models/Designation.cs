using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Designation Name in English")]
        public string Name_En { get; set; }
        [Required(ErrorMessage = "Enter Designation Name in Hindi")]
        public string Name_Hi { get; set; }
        [Required(ErrorMessage = "Enter Priority")]
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}