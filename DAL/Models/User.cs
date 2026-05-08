using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<SalaryRevision> SalaryRevisions { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }

        public User()
        {
            IsActive = true;
            Role = "Employee";
            CreatedAt = DateTime.Now;
            SalaryRevisions = new List<SalaryRevision>();
            Payrolls = new List<Payroll>();
        }
    }
}
