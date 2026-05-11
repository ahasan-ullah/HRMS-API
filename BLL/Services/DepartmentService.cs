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
    public class DepartmentService
    {
        public static List<DepartmentDTO> GetAll()
        {
            var data = DataAccessFactory.DepartmentDataAccess().Get();
            var mappedData = MapperConfig.GetMapper().Map<List<DepartmentDTO>>(data);

            for (int i = 0; i < mappedData.Count; i++)
            {
                mappedData[i].EmployeeCount = data[i].Employees.Count(e => e.IsActive);
            }
            return mappedData;
        }

        public static DepartmentDTO GetById(int id)
        {
            var data = DataAccessFactory.DepartmentDataAccess().Get(id);
            if (data == null)
                return null;

            var mappedData = MapperConfig.GetMapper().Map<DepartmentDTO>(data);
            mappedData.EmployeeCount = data.Employees.Count(e => e.IsActive);
            return mappedData;
        }

        public static bool Create(DepartmentDTO department)
        {
            bool nameExists = DataAccessFactory.DepartmentDataAccess().NameExists(department.Name);
            if (nameExists)
                return false;

            var dept = MapperConfig.GetMapper().Map<Department>(department);

            return DataAccessFactory.DepartmentDataAccess().Create(dept);
        }

        public static bool Update(DepartmentDTO department)
        {
            var existingDept = DataAccessFactory.DepartmentDataAccess().Get(department.DepartmentId);
            if (existingDept == null)
                throw new Exception("Department not found");
            bool nameExists = DataAccessFactory.DepartmentDataAccess().NameExists(department.Name, department.DepartmentId);
            if (nameExists)
                throw new Exception("Department name already exists");

            var dept = MapperConfig.GetMapper().Map<DAL.Models.Department>(department);
            return DataAccessFactory.DepartmentDataAccess().Update(dept);
        }

        public static bool Delete(int id)
        {
            var existing= DataAccessFactory.DepartmentDataAccess().Get(id);
            if (existing == null)
                throw new Exception("Department not found");
            bool result = DataAccessFactory.DepartmentDataAccess().Delete(id);

            if(!result)
                throw new Exception("Failed to delete department");
            return result;
        }
    }
}
