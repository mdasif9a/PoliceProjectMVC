using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class ImportantPlace
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public string LocationDetails { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}