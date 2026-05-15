using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        public bool IsActive { get; set; }

        [Required]
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
