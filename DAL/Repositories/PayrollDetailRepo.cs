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
    internal class PayrollDetailRepo : IPayrollDetailRepo
    {
        private readonly HRMSDbContext dbContext;
        public PayrollDetailRepo()
        {
            dbContext = new HRMSDbContext();
        }
        public bool Create(PayrollDetail obj)
        {
            if(obj == null)
            {
                return false;
            }
            dbContext.PayrollDetails.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var detail = dbContext.PayrollDetails.FirstOrDefault(pd => pd.PayrollDetailId == id);
            if (detail == null)
            {
                return false;
            }
            dbContext.PayrollDetails.Remove(detail);
            return dbContext.SaveChanges() > 0;
        }

        public List<PayrollDetail> Get()
        {
            return dbContext.PayrollDetails.Include(pd=>pd.Employee).Include(pd=>pd.Employee.Department).Include(pd=>pd.Payroll).ToList();
        }

        public PayrollDetail Get(int id)
        {
            return dbContext.PayrollDetails.Include(pd => pd.Employee).Include(pd => pd.Employee.Department).Include(pd => pd.Payroll).FirstOrDefault(pd => pd.PayrollDetailId == id);
        }

        public List<PayrollDetail> GetByPayroll(int payrollId)
        {
            return dbContext.PayrollDetails.Include(pd => pd.Employee).Include(pd => pd.Employee.Department).Include(pd => pd.Payroll).Where(pd => pd.PayrollId == payrollId).ToList();
        }

        public PayrollDetail GetByPayrollAndEmployee(int payrollId, int employeeId)
        {
            return dbContext.PayrollDetails.FirstOrDefault(pd => pd.PayrollId == payrollId && pd.EmployeeId == employeeId);
        }

        public bool Update(PayrollDetail obj)
        {
            var existing = dbContext.PayrollDetails.FirstOrDefault(pd => pd.PayrollDetailId == obj.PayrollDetailId);
            if (existing == null)
            {
                return false;
            }
            existing.PaymentStatus = obj.PaymentStatus;
            existing.BasicSalary = obj.BasicSalary;
            existing.TotalAllowances = obj.TotalAllowances;
            existing.BonusAmount = obj.BonusAmount;
            existing.TotalDeductions = obj.TotalDeductions;
            existing.TaxAmount = obj.TaxAmount;
            existing.NetSalary = obj.NetSalary;
            return dbContext.SaveChanges() > 0;
        }
    }
}
