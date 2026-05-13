using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SalaryRevisionDTO
    {
        public int RevisionId { get; set; }
        public int EmployeeId { get; set; }
        public decimal OldSalary { get; set; }
        public decimal NewSalary { get; set; }
        public decimal Difference { get; set; }
        public DateTime RevisionDate { get; set; }
        public string Reason { get; set; }
        public int RevisedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
