using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SalaryRevision
    {
        public int RevisionId { get; set; }
        public int EmployeeId { get; set; }
        public decimal OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime RevisionDate { get; set; }
        public string Reason { get; set; }
        public int RevisedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual User RevisedByUser { get; set; }

        public SalaryRevision()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
