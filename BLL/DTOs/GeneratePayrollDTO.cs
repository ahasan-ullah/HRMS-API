using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class GeneratePayrollDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int GeneratedBy { get; set; }
    }
}
