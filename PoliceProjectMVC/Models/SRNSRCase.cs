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
        public int MHeadId { get; set; }
        public int SMHeadId { get; set; }
        public int SubDivisionId { get; set; }
        public int CircleId { get; set; }
        public int PoliceStationId { get; set; }
        public string CaseType { get; set; }
        public int ModusOperandiId { get; set; }
        public string FirNo { get; set; }
        public DateTime JailDate { get; set; }
        public string Section { get; set; }
        public DateTime DateAndTimeOfOccurance { get; set; }
        public string PlaceOfOccurance { get; set; }
        public int NoOfKnownAccused { get; set; }
        public int NoOfUnknownAccused { get; set; }
        public string IoName { get; set; }
        public int IoDesignationId { get; set; }
        public string IoMobile { get; set; }
        public string SrNo { get; set; }
        public DateTime CaseDate { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string SupervisionNote { get; set; }
        public DateTime SupervisionDate { get; set; }
        public string SupervisionNoteBy { get; set; }
        public string FinalForm { get; set; }
        public string ProgressReport { get; set; }
        public DateTime ProgressReportDate { get; set; }
        public string CaseDocumentType { get; set; }
        public string CaseDocument { get; set; }
        public string ChargeSheetReport { get; set; }
        public string CaseFinalHead { get; set; }
        public string CurrentCaseStatus { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<Accused> Accuseds { get; set; }
        [NotMapped]
        public Accused Accused { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }

}