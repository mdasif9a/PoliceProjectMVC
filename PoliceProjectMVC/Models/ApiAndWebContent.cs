using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    public class ApiAndWebContent
    {
        [Key]
        public int Id { get; set; }
        public string WebAbout { get; set; }
        public string WebMessageSP { get; set; }
        public string ApiUrl { get; set; }
        public string ApiHeader { get; set; }
    }
}