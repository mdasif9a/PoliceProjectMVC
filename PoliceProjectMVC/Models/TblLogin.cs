using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class TblLogin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Your Number")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Select Role")]
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase MyImage { get; set; }
    }

    public class ViewLogin
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Role { get; set; }
    }
}