using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class BonusDTO
    {
        public int BonusId { get; set; }
        public int EmployeeId { get; set; }
        public string BonusType { get; set; }
        public decimal Amount { get; set; }
        public DateTime BonusDate { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public BonusDTO()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
