using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PayrollDTO
    {
        public int PayrollId { get; set; }
        public int PayrollMonth { get; set; }
        public string MonthName { get; set; }
        public int PayrollYear { get; set; }
        public DateTime GeneratedDate { get; set; }
        public int GeneratedBy { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int TotalEmployees { get; set; }
        public PayrollDTO()
        {
            GeneratedDate = DateTime.Now;
        }
    }
}
