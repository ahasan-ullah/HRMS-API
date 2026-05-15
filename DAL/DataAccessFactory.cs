using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IEmployeeRepo EmployeeDataAccess()
        {
            return new EmployeeRepo();
        }
        public static IDepartmentRepo DepartmentDataAccess()
        {
            return new DepartmentRepo();
        }
        public static ISalaryRepo SalaryDataAccess()
        {
            return new SalaryRepo();
        }
        public static ISalaryRevisionRepo SalaryRevisionDataAccess()
        {
            return new SalaryRevisionRepo();
        }
        public static IDeductionRepo DeductionDataAccess()
        {
            return new DeductionRepo();
        }
        public static IBonusRepo BonusDataAccess()
        {
            return new BonusRepo();
        }
        public static IPayrollRepo PayrollDataAccess()
        {
            return new PayrollRepo();
        }
        public static IPayrollDetailRepo PayrollDetailDataAccess()
        {
            return new PayrollDetailRepo();
        }
    }
}
