using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    [Table("SDPOs")]
    public class SDPO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter SDPO Name in English")]
        public string Name_En { get; set; }
        [Required(ErrorMessage = "Enter SDPO Name in Hindi")]
        public string Name_Hi { get; set; }
        [Required(ErrorMessage = "Select DSP")]
        public int DspId { get; set; }
        [Required(ErrorMessage = "Enter Date of Joining")]
        public DateTime DateOfJoining { get; set; }
        [Required(ErrorMessage = "Enter SDPO Email")]
        public string Sdpo_Email { get; set; }
        [Required(ErrorMessage = "Select Designation")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Enter Mobile")]
        public string MobileNo { get; set; }
        public string AltMobileNo { get; set; }
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
        public Designation Designation { get; set; }
    }
}