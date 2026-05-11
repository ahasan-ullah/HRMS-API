using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string Position { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string EmploymentStatus { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; }

        [Required]
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
            Salaries = new List<Salary>();
            SalaryRevisions = new List<SalaryRevision>();
            Deductions = new List<Deduction>();
            Bonuses = new List<Bonus>();
            PayrollDetails = new List<PayrollDetail>();
        }
    }
}
