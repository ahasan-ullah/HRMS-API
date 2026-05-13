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
    public class DeductionService
    {
        public static List<DeductionDTO> GetAll()
        {
            var data = DataAccessFactory.DeductionDataAccess().Get();
            return MapperConfig.GetMapper().Map<List<DeductionDTO>>(data);
        }
        public static DeductionDTO GetById(int id)
        {
            var data = DataAccessFactory.DeductionDataAccess().Get(id);
            if (data == null) return null;
            return MapperConfig.GetMapper().Map<DeductionDTO>(data);
        }
        public static List<DeductionDTO> GetByEmployeeId(int employeeId)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(employeeId);
            if (employee == null)
                throw new Exception("Employee not found");
            var data = DataAccessFactory.DeductionDataAccess().GetDeductionsByEmployeeId(employeeId);
            return MapperConfig.GetMapper().Map<List<DeductionDTO>>(data);
        }
        public static List<DeductionDTO> GetByEmployeeAndPeriod(int employeeId, int month, int year)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(employeeId);
            if (employee == null)
                throw new Exception("Employee not found");

            if (month < 1 || month > 12)
                throw new Exception("Invalid month.");

            if (year < 1900 || year > 2100)
                throw new Exception("Invalid year.");

            var data = DataAccessFactory.DeductionDataAccess().GetDeductionsByEmployeeIdAndMonthYear(employeeId, month, year);
            return MapperConfig.GetMapper().Map<List<DeductionDTO>>(data);
        }

        public static bool Create(DeductionDTO deduction)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(deduction.EmployeeId);
            if (employee == null)
                throw new Exception("Employee not found");
            if (deduction.Amount <= 0)
                throw new Exception("Deduction amount must be greater than zero.");

            if (deduction.Month < 1 || deduction.Month > 12)
                throw new Exception("Invalid month.");

            if (deduction.Year < 1900 || deduction.Year > 2100)
                throw new Exception("Invalid year.");

            var ded = MapperConfig.GetMapper().Map<Deduction>(deduction);
            return DataAccessFactory.DeductionDataAccess().Create(ded);
        }

        public static bool Update(DeductionDTO deduction)
        {
            var existing = DataAccessFactory.DeductionDataAccess().Get(deduction.DeductionId);
            if (existing == null)
                throw new Exception("Deduction not found");
            if (deduction.Amount <= 0)
                throw new Exception("Deduction amount must be greater than zero.");
            var ded = MapperConfig.GetMapper().Map<Deduction>(deduction);
            return DataAccessFactory.DeductionDataAccess().Update(ded);
        }
        public static bool Delete(int id)
        {
            var existing = DataAccessFactory.DeductionDataAccess().Get(id);
            if (existing == null)
                throw new Exception("Deduction not found");
            return DataAccessFactory.DeductionDataAccess().Delete(id);
        }
    }
}
