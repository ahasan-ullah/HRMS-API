using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PayrollDetail
    {
        [Key]
        public int PayrollDetailId { get; set; }

        [ForeignKey("Payroll")]
        public int PayrollId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal BasicSalary { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal TotalAllowances { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal BonusAmount { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal TotalDeductions { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal TaxAmount { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal NetSalary { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; }

        public virtual Payroll Payroll { get; set; }

        public virtual Employee Employee { get; set; }

        public PayrollDetail()
        {
            PaymentStatus = "Pending";
            TotalAllowances = 0;
            BonusAmount = 0;
            TotalDeductions = 0;
            TaxAmount = 0;
        }
    }
}
