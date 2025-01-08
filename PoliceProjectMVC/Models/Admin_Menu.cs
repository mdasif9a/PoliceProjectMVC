using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceProjectMVC.Models
{
    [Table("Admin_Menus")]
    public class Admin_Menu
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuName_En { get; set; }
        public string MenuURL { get; set; }
        public int MenuOrder { get; set; }
        public int Menu_ParentId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDropDown { get; set; }
        public int RoleId { get; set; }
        public string IconClass { get; set; }

        [NotMapped]
        public List<Admin_Menu> SubMenus { get; set; } = new List<Admin_Menu>();
    }

}