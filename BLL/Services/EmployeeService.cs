using BLL.DTOs;
using BLL.Helpers;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService
    {
        public static List<EmployeeDTO> GetAll()
        {
            var data = DataAccessFactory.EmployeeDataAccess().Get();
            return MapperConfig.GetMapper().Map<List<EmployeeDTO>>(data);
        }

        public static EmployeeDTO GetById(int id)
        {
            var data = DataAccessFactory.EmployeeDataAccess().Get(id);
            return MapperConfig.GetMapper().Map<EmployeeDTO>(data);
        }

        public static List<EmployeeDTO> Search(string keyword)
        {
            var employees = DataAccessFactory.EmployeeDataAccess().Get();
            if (string.IsNullOrEmpty(keyword))
            {
                return MapperConfig.GetMapper().Map<List<EmployeeDTO>>(employees);
            }
            keyword = keyword.Trim().ToLower();
            employees = employees.Where(e => e.FirstName.ToLower().Contains(keyword) || e.LastName.ToLower().Contains(keyword) || e.Email.ToLower().Contains(keyword) || e.Department.Name.ToLower().Contains(keyword) || e.Position.ToLower().Contains(keyword)).ToList();
            return MapperConfig.GetMapper().Map<List<EmployeeDTO>>(employees);
        }

        public static List<EmployeeDTO> FilterByDepartment(int departmentId)
        {
            var employees = DataAccessFactory.EmployeeDataAccess().Get();
            if (departmentId <= 0)
            {
                return MapperConfig.GetMapper().Map<List<EmployeeDTO>>(employees);
            }
            employees = employees.Where(e => e.DepartmentId == departmentId).ToList();
            return MapperConfig.GetMapper().Map<List<EmployeeDTO>>(employees);
        }

        public static bool Create(EmployeeDTO employee)
        {
            var dept = DataAccessFactory.DepartmentDataAccess().Get(employee.DepartmentId);
            if(dept == null)
            {
                throw new Exception("Department not found");
            }

            bool emailExists=DataAccessFactory.EmployeeDataAccess().EmailExists(employee.Email);
            if(emailExists)
            {
                throw new Exception("Email already exists");
            }

            bool accountExists = DataAccessFactory.EmployeeDataAccess().AccountNumberExists(employee.AccountNumber);
            if (accountExists)
            {
                throw new Exception("Account number already exists");
            }

            var emp = MapperConfig.GetMapper().Map<Employee>(employee);
            bool created= DataAccessFactory.EmployeeDataAccess().Create(emp);
            if (created)
            {
                var saved = DataAccessFactory.EmployeeDataAccess()
                    .GetByEmail(employee.Email);

                if (saved != null)
                    AuthService.CreateEmployeeUser(saved.EmployeeId, saved.FirstName);
            }
            return created;
        }

        public static bool Update(EmployeeDTO employee)
        {
            var dept = DataAccessFactory.DepartmentDataAccess().Get(employee.DepartmentId);
            if (dept == null)
            {
                throw new Exception("Department not found");
            }
            bool emailExists = DataAccessFactory.EmployeeDataAccess().EmailExists(employee.Email, employee.EmployeeId);
            if (emailExists)
            {
                throw new Exception("Email already exists");
            }
            bool accountExists = DataAccessFactory.EmployeeDataAccess().AccountNumberExists(employee.AccountNumber, employee.EmployeeId);
            if (accountExists)
            {
                throw new Exception("Account number already exists");
            }
            var emp = MapperConfig.GetMapper().Map<Employee>(employee);
            return DataAccessFactory.EmployeeDataAccess().Update(emp);
        }

        public static bool Delete(int id)
        {
            var emp = DataAccessFactory.EmployeeDataAccess().Get(id);
            if (emp == null)
            {
                throw new Exception("Employee not found");
            }
            return DataAccessFactory.EmployeeDataAccess().Delete(id);
        }
    }
}
