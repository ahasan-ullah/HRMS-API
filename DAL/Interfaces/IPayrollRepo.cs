using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPayrollRepo:IRepo<Payroll, int, bool>
    {
        Payroll GetByMonthYear(int month, int year);
        bool ExistsForPeriod(int month, int year);
    }
}
