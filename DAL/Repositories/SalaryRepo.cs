using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class SalaryRepo : ISalaryRepo
    {
        public readonly HRMSDbContext dbContext;
        public SalaryRepo()
        {
            dbContext = new HRMSDbContext();
        }
        public bool Create(Salary obj)
        {
            if (obj == null)
                return false;
            dbContext.Salaries.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var salary = dbContext.Salaries.Find(id);
            if (salary == null) return false;

            salary.IsActive = false;
            return dbContext.SaveChanges() > 0;
        }

        public List<Salary> Get()
        {
            return dbContext.Salaries.ToList();
        }

        public Salary Get(int id)
        {
            return dbContext.Salaries.FirstOrDefault(s => s.SalaryId == id);
        }

        public Salary GetActiveSalaryByEmployeeId(int employeeId)
        {
            return dbContext.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId && s.IsActive);
        }

        public bool HasActiveSalary(int employeeId)
        {
            return dbContext.Salaries.Any(s => s.EmployeeId == employeeId && s.IsActive);
        }

        public bool Update(Salary obj)
        {
            var existingSalary = dbContext.Salaries.FirstOrDefault(s => s.SalaryId == obj.SalaryId);
            if (existingSalary == null)
                return false;

            dbContext.Entry(existingSalary).CurrentValues.SetValues(obj);
            return dbContext.SaveChanges() > 0;
        }
    }
}
