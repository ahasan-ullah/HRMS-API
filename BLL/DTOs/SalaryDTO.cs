using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SalaryDTO
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal TransportAllowance { get; set; }
        public decimal GrossSalary { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public SalaryDTO()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
