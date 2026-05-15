using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPayrollDetailRepo:IRepo<PayrollDetail, int, bool>
    {
        List<PayrollDetail> GetByPayroll(int payrollId);
        PayrollDetail GetByPayrollAndEmployee(int payrollId, int employeeId);
    }
}
