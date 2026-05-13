using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class DeductionRepo : IDeductionRepo
    {
        private readonly HRMSDbContext dbContext;

        public DeductionRepo()
        {
            dbContext = new HRMSDbContext();
        }

        public bool Create(Deduction obj)
        {
            if(obj == null)
            {
                return false;
            }

            dbContext.Deductions.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var deduction = dbContext.Deductions.FirstOrDefault(d => d.DeductionId == id);
            if (deduction == null) return false;

            dbContext.Deductions.Remove(deduction);
            return dbContext.SaveChanges() > 0;
        }

        public List<Deduction> Get()
        {
            return dbContext.Deductions.Include(d => d.Employee).ToList();
        }

        public Deduction Get(int id)
        {
            return dbContext.Deductions.Include(d => d.Employee).FirstOrDefault(d => d.DeductionId == id);
        }

        public List<Deduction> GetDeductionsByEmployeeId(int employeeId)
        {
            return dbContext.Deductions.Where(d => d.EmployeeId == employeeId).Include(d => d.Employee).ToList();
        }

        public List<Deduction> GetDeductionsByEmployeeIdAndMonthYear(int employeeId, int month, int year)
        {
            return dbContext.Deductions.Where(d => d.EmployeeId == employeeId && d.Month == month && d.Year == year).Include(d => d.Employee).ToList();
        }

        public bool Update(Deduction obj)
        {
            var existing = dbContext.Deductions.FirstOrDefault(d => d.DeductionId == obj.DeductionId);
            if (existing == null) return false;

            existing.DeductionType = obj.DeductionType;
            existing.Amount = obj.Amount;
            existing.Month = obj.Month;
            existing.Year = obj.Year;
            existing.Reason = obj.Reason;

            return dbContext.SaveChanges() > 0;
        }
    }
}
