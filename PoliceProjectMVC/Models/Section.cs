using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Section Name in English")]
        public string Name_En { get; set; }
        [Required(ErrorMessage = "Enter Section Name in Hindi")]
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