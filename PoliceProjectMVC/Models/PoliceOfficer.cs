using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class PoliceOfficer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter BrassNo")]
        public string BrassNo { get; set; }
        [Required(ErrorMessage = "Select Designation")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Mobile No.")]
        [Phone(ErrorMessage = "Enter Valid Mobile No.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Enter Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Enter Date Of Joining")]
        public DateTime DateOfJoining { get; set; }
        [Required(ErrorMessage = "Enter Home District")]
        public string HomeDistrict { get; set; }
        [Required(ErrorMessage = "Enter Place of Posting")]
        public string PlaceOfPosting { get; set; }
        [Required(ErrorMessage = "Enter Name of Posting")]
        public string NameOfPosting { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}