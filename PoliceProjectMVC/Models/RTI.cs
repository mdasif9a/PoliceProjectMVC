using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class RTI
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Serial Number and Date")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Enter Request Subject")]
        public string RequestSubject { get; set; }

        [Required(ErrorMessage = "Enter Disposal Target Date")]
        public DateTime DisposalTargetDate { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Enter Source (Got It From)")]
        public string GotItFrom { get; set; }

        [Required(ErrorMessage = "Enter Applicant Name and Address")]
        public string ApplicantName { get; set; }

        [Required(ErrorMessage = "Enter Received Letter from confidential branch & date")]
        public string ReceivedLetter { get; set; }

        [Required(ErrorMessage = "Select Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Enter Execution Date And Knowledge Book")]
        public string ExecutionDateAndKnowledgeBook { get; set; }

        [Required(ErrorMessage = "Enter Conviction")]
        public string Conviction { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }

}