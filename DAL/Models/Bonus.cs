using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Bonus
    {
        [Key]
        public int BonusId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string BonusType { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime BonusDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }

        public Bonus()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
