using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ComplainSubject { get; set; }
        public string ComplainType { get; set; }
        public string ComplainDetails { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Mobile No")]
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string PermanentAdd { get; set; }
        public string CorrespoAdd { get; set; }
        public string District { get; set; }
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