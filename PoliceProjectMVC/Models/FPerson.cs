using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class FPerson
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Person Name")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Select Police Station")]
        public int PoliceStationId { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        public string PersonGender { get; set; }

        [Required(ErrorMessage = "Enter Age")]
        public int PersonAge { get; set; }

        [Required(ErrorMessage = "Enter Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Enter Height")]
        public string PersonHeight { get; set; }

        [Required(ErrorMessage = "Enter Color")]
        public string PersonColor { get; set; }

        [Required(ErrorMessage = "Enter Person Identity")]
        public string PersonIdentity { get; set; }

        [Required(ErrorMessage = "Enter Found Area")]
        public string FoundArea { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Enter Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Enter Mother Name")]
        public string MotherName { get; set; }

        public string WearingClothes { get; set; }

        [Required(ErrorMessage = "Enter Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Enter Found Date")]
        public DateTime FoundDate { get; set; }

        [Required(ErrorMessage = "Enter Fir No Date")]
        public string FIRNoAndDate { get; set; }

        [Required(ErrorMessage = "Enter Mobile No")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        public string Religion { get; set; }
        public string SectionLodged { get; set; }
        public string Language { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
        public PoliceStation PoliceStation { get; set; }
    }



}