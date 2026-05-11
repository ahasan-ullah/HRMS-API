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
    internal class EmployeeRepo : IEmployeeRepo
    {
        private readonly HRMSDbContext dbContext;

        public EmployeeRepo()
        {
            dbContext = new HRMSDbContext();
        }
        public bool AccountNumberExists(string accountNumber, int? excludeEmployeeId = null)
        {
            if(excludeEmployeeId==null)
                return dbContext.Employees.Any(e => e.AccountNumber == accountNumber);

            return dbContext.Employees.Any(e => e.AccountNumber == accountNumber && e.EmployeeId != excludeEmployeeId);
        }

        public bool Create(Employee obj)
        {
            if(obj == null)
                return false;
            dbContext.Employees.Add(obj);
            dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
                return false;
            employee.IsActive = false;
            employee.EmploymentStatus="Terminated";
            employee.UpdatedAt = DateTime.Now;
            dbContext.Entry(employee).CurrentValues.SetValues(employee);
            return dbContext.SaveChanges() > 0;
        }

        public bool EmailExists(string email, int? excludeEmployeeId = null)
        {
            if(excludeEmployeeId == null)
                return dbContext.Employees.Any(e => e.Email == email);

            return dbContext.Employees.Any(e => e.Email == email && e.EmployeeId != excludeEmployeeId);
        }

        public List<Employee> Get()
        {
            return dbContext.Employees.Where(e => e.IsActive).ToList();
        }

        public Employee Get(int id)
        {
            return dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id && e.IsActive);
        }

        public bool Update(Employee obj)
        {
            var existingEmployee = dbContext.Employees.Find(obj.EmployeeId);
            if (existingEmployee == null)
                return false;
            existingEmployee.UpdatedAt = DateTime.Now;
            dbContext.Entry(existingEmployee).CurrentValues.SetValues(obj);
            return dbContext.SaveChanges()>0;
        }
    }
}
