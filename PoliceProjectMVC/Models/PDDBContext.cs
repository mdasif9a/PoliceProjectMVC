using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PoliceProjectMVC.Models
{
    public class PDDBContext : DbContext
    {
        public PDDBContext() : base("name=PDDBContext") { }
        public virtual DbSet<M_Menu> M_Menus { get; set; }
        public virtual DbSet<Admin_Menu> Admin_Menus { get; set; }
        public virtual DbSet<TblLogin> TblLogins { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblOurTeam> TblOurTeams { get; set; }
        public virtual DbSet<TblHelplineNumber> TblHelplineNumbers { get; set; }
    }
}