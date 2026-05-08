using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string AccountNumber { get; set; }
        public string EmploymentStatus { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<SalaryRevision> SalaryRevisions { get; set; }
        public virtual ICollection<Deduction> Deductions { get; set; }
        public virtual ICollection<Bonus> Bonuses { get; set; }
        public virtual ICollection<PayrollDetail> PayrollDetails { get; set; }

        public Employee()
        {
            IsActive = true;
            EmploymentStatus = "Active";
            CreatedAt = DateTime.Now;
            Salaries = new List<Salary>();
            SalaryRevisions = new List<SalaryRevision>();
            Deductions = new List<Deduction>();
            Bonuses = new List<Bonus>();
            PayrollDetails = new List<PayrollDetail>();
        }
    }
}
