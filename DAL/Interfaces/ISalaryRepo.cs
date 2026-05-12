using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISalaryRepo:IRepo<Salary,int,bool>
    {
        Salary GetActiveSalaryByEmployeeId(int employeeId);
        bool HasActiveSalary(int employeeId);
    }
}
