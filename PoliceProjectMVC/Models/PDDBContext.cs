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
        public virtual DbSet<BestEmployee> BestEmployees { get; set; }
        public virtual DbSet<IAcheveMent> IAcheveMents { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<MWCriminal> MWCriminals { get; set; }
        public virtual DbSet<SuccessionList> SuccessionLists { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<DSP> DSPs { get; set; }
        public virtual DbSet<SDPO> SDPOs { get; set; }
        public virtual DbSet<SubDivision> SubDivisions { get; set; }
        public virtual DbSet<Circle> Circles { get; set; }
        public virtual DbSet<PoliceOfficer> PoliceOfficers { get; set; }
        public virtual DbSet<Passport> Passports { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<GrievancePolice> GrievancePolices { get; set; }
    }
}