using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Deduction
    {
        [Key]
        public int DeductionId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeductionType { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal Amount { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }

        public Deduction()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
