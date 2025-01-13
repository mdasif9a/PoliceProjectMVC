using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Title")]
        public string Title { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }

}