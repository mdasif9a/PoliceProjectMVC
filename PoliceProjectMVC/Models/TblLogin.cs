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
        [Required(ErrorMessage = "Enter Your Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        public string Password { get; set; }
        public string Number { get; set; }
        public string Role { get; set; }
    }
}