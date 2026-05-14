using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal BasicSalary { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal HouseAllowance { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal MedicalAllowance { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal TransportAllowance { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }

        public Salary()
        {
            IsActive = true;
            HouseAllowance = 0;
            MedicalAllowance = 0;
            TransportAllowance = 0;
        }
    }
}
