using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Accused
    {
        [Key]
        public int Id { get; set; }
        public int SRNSRCaseId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }     
        public string Chargesheet { get; set; }
        public int Age { get; set; }
        public string ConfessionStatement { get; set; }
        public DateTime ChargesheetDate { get; set; }
        public string ImageOfAccused { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase ConfessionFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AccusedFile { get; set; }
    }

}