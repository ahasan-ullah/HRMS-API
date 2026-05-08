using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal TransportAllowance { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }

        public Salary()
        {
            IsActive = true;
            HouseAllowance = 0;
            MedicalAllowance = 0;
            TransportAllowance = 0;
            CreatedAt = DateTime.Now;
        }
    }
}
