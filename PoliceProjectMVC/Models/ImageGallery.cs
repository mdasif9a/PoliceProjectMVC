﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class ImageGallery
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Title")]
        public string Title { get; set; }
        public int EventId { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}