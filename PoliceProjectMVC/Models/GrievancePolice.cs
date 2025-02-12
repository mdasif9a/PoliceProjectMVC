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
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string CurrentPosting { get; set; }
        public string CurrentStatus { get; set; }
        public bool GenderType { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Enter Incident Date")]
        public DateTime IncidentDate { get; set; }
        public string UpdatedBy { get; set; }
        [Required(ErrorMessage = "Select Police Station")]
        public int PoliceStationId { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Upload File")]
        public HttpPostedFileBase MyImage { get; set; }
        public PoliceStation PoliceStation { get; set; }
    }

}