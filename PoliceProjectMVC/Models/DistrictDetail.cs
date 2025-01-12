using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class DistrictDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string VisitorNo { get; set; }
        public string Email { get; set; }
        public string MapUrl { get; set; }
        public string ContactNo { get; set; }
        public string MobileNo { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
        public string DownloadLink { get; set; }
        public string QRImageUrl { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }



}