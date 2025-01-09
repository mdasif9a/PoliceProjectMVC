using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class SubDivision
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name in English")]
        public string Name_En { get; set; }
        [Required(ErrorMessage = "Enter Name in Hindi")]
        public string Name_Hi { get; set; }
        public string Description_En { get; set; }
        public string Description_Hi { get; set; }
        [Required(ErrorMessage = "Select SDPO")]
        public int SdpoId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}