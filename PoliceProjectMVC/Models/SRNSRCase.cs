using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class SRNSRCase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Select Major Head")]
        public int MHeadId { get; set; }
        [Required(ErrorMessage = "Select Sub Major Head")]
        public int SMHeadId { get; set; }
        [Required(ErrorMessage = "Select Sub Division")]
        public int SubDivisionId { get; set; }
        [Required(ErrorMessage = "Select Circle")]
        public int CircleId { get; set; }
        [Required(ErrorMessage = "SelectPolice Station")]
        public int PoliceStationId { get; set; }
        public string CaseType { get; set; }
        [Required(ErrorMessage = "Select Modus Operandi")]
        public int ModusOperandiId { get; set; }
        public string FirNo { get; set; }
        public DateTime JailDate { get; set; }
        [Required(ErrorMessage = "Enter  Section")]
        public string Section { get; set; }
        public DateTime DateAndTimeOfOccurance { get; set; }
        public string PlaceOfOccurance { get; set; }
        public int NoOfKnownAccused { get; set; } = 0;
        public int? NoOfUnknownAccused { get; set; } = 0;
        public string IoName { get; set; }
        public int? IoDesignationId { get; set; }
        public string IoMobile { get; set; }
        public string CIMobile { get; set; }
        [Required(ErrorMessage = "Select SR No/ Case No")]
        public string SrNo { get; set; }
        public DateTime CaseDate { get; set; }
        public string ReportType { get; set; }
        public DateTime? ReportDate { get; set; }
        public string SupervisionNote { get; set; }
        public DateTime? SupervisionDate { get; set; }
        public string SupervisionNoteBy { get; set; }
        public string FinalForm { get; set; }
        public string ProgressReport { get; set; }
        public DateTime? ProgressReportDate { get; set; }
        public string CaseDocumentType { get; set; }
        public string CaseDocument { get; set; }
        public string ChargeSheetReport { get; set; }
        public string CaseFinalHead { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool SecondWPMessageSent { get; set; }
        public bool ThirdWPMessageSent { get; set; }
        public bool FourthWPMessageSent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastChargeSheetdate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string AccusedName { get; set; }
        public string AccusedAddress { get; set; }
        public string ChargesheetYesNo { get; set; }
        public string ConfessionStatement { get; set; }
        public DateTime? ChargesheetDate { get; set; }
        public string ImageOfAccused { get; set; }

        public MHead MHead { get; set; }
        public PoliceStation PoliceStation { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
        [NotMapped]
        public HttpPostedFileBase ConfessionFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AccusedFile { get; set; }
    }

}