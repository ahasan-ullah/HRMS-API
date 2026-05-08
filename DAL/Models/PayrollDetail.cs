using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PayrollDetail
    {
        public int PayrollDetailId { get; set; }
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal BonusAmount { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetSalary { get; set; }
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
