using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Link Name in Hindi")]
        public string LinkName_Hi { get; set; }
        [Required(ErrorMessage = "Enter Link Name in English")]
        public string LinkName_En { get; set; }
        public string Description_Hi { get; set; }
        public string Description_En { get; set; }
        [Required(ErrorMessage = "Select Link Type")]
        public int LinkType { get; set; }
        [Required(ErrorMessage = "Enter Link URL")]
        public string LinkUrl { get; set; }
        public string Color { get; set; }
        [Required(ErrorMessage = "Enter Icon Class")]
        public string Icon { get; set; }
        [Required(ErrorMessage = "Enter Priority")]
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }


}