using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class MOperandi
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Select Major Head")]
        public int MHeadId { get; set; }

        [Required(ErrorMessage = "Select Sub Major Head")]
        public int SMHeadId { get; set; }

        [Required(ErrorMessage = "Enter Modus Operandi")]
        public string Name_EN { get; set; }
        [Required(ErrorMessage = "Enter Modus Operandi in Hindi")]

        public string Name_Hi { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

}