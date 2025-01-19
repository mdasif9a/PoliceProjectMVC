using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    [Table("GrievancePolices")]
    public class GrievancePolice
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Employee ID")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Mobile No")]
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Current Posting")]
        public string CurrentPosting { get; set; }
        [Required(ErrorMessage = "Select Current Status")]
        public string CurrentStatus { get; set; }
        public bool GenderType { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime IncidentDate { get; set; }
        public string UpdatedBy { get; set; }
        public int PoliceStationId { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
        public PoliceStation PoliceStation { get; set; }
    }

}