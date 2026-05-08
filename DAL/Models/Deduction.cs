using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Deduction
    {
        public int DeductionId { get; set; }
        public int EmployeeId { get; set; }
        public string DeductionType { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Employee Employee { get; set; }

        public Deduction()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
