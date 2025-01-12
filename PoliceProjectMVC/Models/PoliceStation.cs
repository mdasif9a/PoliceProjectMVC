using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class PoliceStation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Station Name in English")]
        public string Name_En { get; set; }
        [Required(ErrorMessage = "Enter Station Name in Hindi")]
        public string Name_Hi { get; set; }
        [Required(ErrorMessage = "Select Circle")]
        public int CircleId { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Mobile Number")]
        public string MobileNo { get; set; }
        public string HelpLineNo { get; set; }
        public string MedicalNo { get; set; }
        public string AmbulanceNo { get; set; }
        public string FieBrigadeNo { get; set; }
        public string SiteMapURL { get; set; }
        public string AreaCovered { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
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
        public Circle Circle { get; set; }
    }

}