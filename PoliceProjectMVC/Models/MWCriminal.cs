using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class MWCriminal
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name in Hindi")]
        public string Name_Hi { get; set; }
        [Required(ErrorMessage = "Enter Name in English")]
        public string Name_En { get; set; }
        public string Description_Hi { get; set; }
        public string Description_En { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Enter Priority")]
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }


}