using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class MHead
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Major Head")]
        public string Name_EN { get; set; }
        [Required(ErrorMessage = "Enter Major Head in Hindi")]
        public string Name_Hi { get; set; }
        [Required(ErrorMessage = "Enter Chargesheet Monitoring Days")]
        public int CSM_Days { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

}