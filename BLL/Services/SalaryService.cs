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
    public class SalaryService
    {
        public static List<SalaryDTO> GetAll()
        {
            var data = DataAccessFactory.SalaryDataAccess().Get();
            return MapperConfig.GetMapper().Map<List<SalaryDTO>>(data);
        }
        public static SalaryDTO GetById(int id)
        {
            var data = DataAccessFactory.SalaryDataAccess().Get(id);
            return MapperConfig.GetMapper().Map<SalaryDTO>(data);
        }
        public static SalaryDTO GetActiveSalaryByEmployeeId(int employeeId)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            var data = DataAccessFactory.SalaryDataAccess().GetActiveSalaryByEmployeeId(employeeId);
            if (data == null)
            {
                return null;
            }
            return MapperConfig.GetMapper().Map<SalaryDTO>(data);
        }
        public static bool Create(SalaryDTO salary)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(salary.EmployeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            if (salary.BasicSalary <= 0)
            {
                throw new Exception("Basic Salary must be greater than zero");
            }
            var activeSalary = DataAccessFactory.SalaryDataAccess().GetActiveSalaryByEmployeeId(salary.EmployeeId);
            if (activeSalary != null)
            {
                activeSalary.IsActive = false;
                DataAccessFactory.SalaryDataAccess().Update(activeSalary);
            }
            var data = MapperConfig.GetMapper().Map<Salary>(salary);
            return DataAccessFactory.SalaryDataAccess().Create(data);
        }
        public static bool Update(SalaryDTO salary)
        {
            var existingSalary = DataAccessFactory.SalaryDataAccess().Get(salary.SalaryId);
            if (existingSalary == null)
            {
                throw new Exception("Salary not found");
            }
            if (salary.BasicSalary <= 0)
            {
                throw new Exception("Basic Salary must be greater than zero");
            }
            var data = MapperConfig.GetMapper().Map<Salary>(salary);
            return DataAccessFactory.SalaryDataAccess().Update(data);
        }
        public static bool ReviseSalary(SalaryRevisionDTO obj)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(obj.EmployeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            var activeSalary = DataAccessFactory.SalaryDataAccess().GetActiveSalaryByEmployeeId(obj.EmployeeId);
            if (activeSalary == null)
            {
                throw new Exception("Active salary not found for the employee");
            }
            if (obj.NewSalary <= 0)
            {
                throw new Exception("New Salary must be greater than zero");
            }
            if (obj.NewSalary == activeSalary.BasicSalary)
            {
                throw new Exception("New Salary must be different from the current salary");
            }
            var salaryRevision = new SalaryRevision
            {
                EmployeeId = obj.EmployeeId,
                OldSalary = activeSalary.BasicSalary,
                NewSalary = obj.NewSalary,
                Reason = obj.Reason,
                RevisedBy = obj.RevisedBy
            };
            DataAccessFactory.SalaryRevisionDataAccess().Create(salaryRevision);
            activeSalary.IsActive = false;
            DataAccessFactory.SalaryDataAccess().Update(activeSalary);

            var newSalary = new Salary
            {
                EmployeeId = obj.EmployeeId,
                BasicSalary = obj.NewSalary,
                HouseAllowance = activeSalary.HouseAllowance,
                MedicalAllowance = activeSalary.MedicalAllowance,
                TransportAllowance = activeSalary.TransportAllowance,
                IsActive = true,
                Remarks = "Revised salary"
            };
            return DataAccessFactory.SalaryDataAccess().Create(newSalary);
        }
        public static List<SalaryRevisionDTO> GetSalaryRevisionsByEmployeeId(int employeeId)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            var revisions = DataAccessFactory.SalaryRevisionDataAccess().GetSalaryRevisionsByEmployeeId(employeeId);
            return MapperConfig.GetMapper().Map<List<SalaryRevisionDTO>>(revisions);
        }
        public static bool Delete(int id)
        {
            var existingSalary = DataAccessFactory.SalaryDataAccess().Get(id);
            if (existingSalary == null)
            {
                throw new Exception("Salary not found");
            }
            return DataAccessFactory.SalaryDataAccess().Delete(id);
        }
    }
}
