using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceProjectMVC.Models
{
    public class SMHead
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Select Major Head")]
        public int MHeadId { get; set; }

        [Required(ErrorMessage = "Enter Sub Major Head")]
        public string Name_EN { get; set; }
        [Required(ErrorMessage = "Enter Sub Major Head in Hindi")]

        public string Name_Hi { get; set; }

        public bool IsAccused { get; set; } = false;

        public bool IsDeceased { get; set; } = false;

        public bool IsVictim { get; set; } = false;

        public bool IsCashCollection { get; set; } = false;

        public bool IsLootedItems { get; set; } = false;

        public bool IsOtherEvidence { get; set; } = false;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public MHead MHead { get; set; }
    }

}