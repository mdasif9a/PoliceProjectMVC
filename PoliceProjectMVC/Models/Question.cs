using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Question")]
        public string QuestionText { get; set; }
        [Required(ErrorMessage = "Enter Answer 1")]
        public string Answer1 { get; set; }
        [Required(ErrorMessage = "Enter Answer 2")]
        public string Answer2 { get; set; }
        [Required(ErrorMessage = "Enter Answer 3")]
        public string Answer3 { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}