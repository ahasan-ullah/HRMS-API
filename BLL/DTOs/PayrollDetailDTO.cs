using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PayrollDetailDTO
    {
        public int PayrollDetailId { get; set; }
        public int PayrollId { get; set; }
        public int PayrollMonth { get; set; }
        public int PayrollYear { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal BonusAmount { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetSalary { get; set; }
        public string PaymentStatus { get; set; }
    }
}
