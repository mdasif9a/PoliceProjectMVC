using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rel_Name { get; set; }
        public string MobileNo { get; set; }
        public int PoliceStationId { get; set; }
        public string ApplicationType { get; set; }
        public string RecieptNo { get; set; }
        public string DistrictMemoNo { get; set; }
        public string ComplaintSource { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }


}