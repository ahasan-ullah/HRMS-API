using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payroll
    {
        public int PayrollId { get; set; }
        public int PayrollMonth { get; set; }
        public int PayrollYear { get; set; }
        public DateTime GeneratedDate { get; set; }
        public int GeneratedBy { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public virtual User GeneratedByUser { get; set; }
        public virtual ICollection<PayrollDetail> PayrollDetails { get; set; }

        public Payroll()
        {
            GeneratedDate = DateTime.Now;
            Status = "Draft";
            TotalAmount = 0;
            PayrollDetails = new List<PayrollDetail>();
        }
    }
}
