using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SalaryRevision
    {
        [Key]
        public int RevisionId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal OldSalary { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal NewSalary { get; set; }

        [Required]
        public DateTime RevisionDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [ForeignKey("RevisedByUser")]
        public int RevisedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual User RevisedByUser { get; set; }

        public SalaryRevision()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
