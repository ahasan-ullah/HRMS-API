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
    }
}
