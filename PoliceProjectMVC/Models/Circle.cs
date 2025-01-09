using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Circle
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Circle Name in English")]
        public string Name_En { get; set; }
        [Required(ErrorMessage = "Enter Circle Name in Hindi")]
        public string Name_Hi { get; set; }
        [Required(ErrorMessage = "Enter Office Name in English")]
        public string OName_En { get; set; }
        [Required(ErrorMessage = "Enter Office Name in Hindi")]
        public string OName_Hi { get; set; }
        [Required(ErrorMessage = "Select SubDisvisioin")]
        public int SubDivId { get; set; }
        [Required(ErrorMessage = "Enter Date Of Joining")]
        public DateTime DateOfJoining { get; set; }
        [Required(ErrorMessage = "Enter Circle Email")]
        public string Circle_Email { get; set; }
        [Required(ErrorMessage = "Select Designation")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Enter Mobile No.")]
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
    }
}