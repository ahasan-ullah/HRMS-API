using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class DepartmentRepo : IDepartmentRepo
    {
        private readonly HRMSDbContext dbContext;

        public DepartmentRepo()
        {
            dbContext= new HRMSDbContext();
        }
        public bool Create(Department obj)
        {
            if(obj == null)
                return false;
            
            dbContext.Departments.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var department = dbContext.Departments.Find(id);
            if(department == null)
                return false;

            bool hasActiveEmployees = dbContext.Employees.Any(e => e.DepartmentId == id && e.IsActive);
            if(hasActiveEmployees)
                return false;

            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges() > 0;
        }

        public List<Department> Get()
        {
            return dbContext.Departments.ToList();
        }

        public Department Get(int id)
        {
            return dbContext.Departments.FirstOrDefault(d => d.DepartmentId == id);
        }

        public bool NameExists(string name, int? excludeDepartmentId = null)
        {
            if(excludeDepartmentId==null)
                return dbContext.Departments.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return dbContext.Departments.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && d.DepartmentId != excludeDepartmentId.Value);
        }

        public bool Update(Department obj)
        {
            if(obj == null)
                return false;

            var existingDepartment = dbContext.Departments.Find(obj.DepartmentId);
            if(existingDepartment == null)
                return false;

            existingDepartment.Name = obj.Name;
            existingDepartment.Description = obj.Description;

            return dbContext.SaveChanges() > 0;
        }
    }
}
