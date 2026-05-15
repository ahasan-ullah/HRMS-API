using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; }

        [Range(1, 12)]
        public int PayrollMonth { get; set; }

        [Range(1900, 2100)]
        public int PayrollYear { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }

        [ForeignKey("GeneratedByUser")]
        public int GeneratedBy { get; set; }

        [Range(typeof(decimal), "0", "999999999999.99")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public virtual User GeneratedByUser { get; set; }

        public virtual ICollection<PayrollDetail> PayrollDetails { get; set; }

        public Payroll()
        {
            GeneratedDate = DateTime.Now;
            Status = HRMSEnums.PayrollStatus.Draft.ToString();
            TotalAmount = 0;
            PayrollDetails = new List<PayrollDetail>();
        }
    }
}
