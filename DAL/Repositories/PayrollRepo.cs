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
    internal class PayrollRepo : IPayrollRepo
    {
        private readonly HRMSDbContext dbContext;
        public PayrollRepo()
        {
            dbContext = new HRMSDbContext();
        }
        public bool Create(Payroll obj)
        {
            if(obj == null)
                return false;

            dbContext.Payrolls.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var payroll = dbContext.Payrolls.FirstOrDefault(p => p.PayrollId == id);
            if (payroll == null)
                return false;
            if (payroll.Status != "Draft") return false;
            dbContext.Payrolls.Remove(payroll);
            return dbContext.SaveChanges() > 0;
        }

        public bool ExistsForPeriod(int month, int year)
        {
            return dbContext.Payrolls.Any(p => p.PayrollMonth == month && p.PayrollYear == year);
        }

        public List<Payroll> Get()
        {
            return dbContext.Payrolls.Include(p => p.GeneratedByUser).Include(p => p.PayrollDetails).ToList();
        }

        public Payroll Get(int id)
        {
            return dbContext.Payrolls.Include(p => p.GeneratedByUser).Include(p => p.PayrollDetails).FirstOrDefault(p => p.PayrollId == id);
        }

        public Payroll GetByMonthYear(int month, int year)
        {
            return dbContext.Payrolls.Include(p => p.PayrollDetails)
                .FirstOrDefault(p => p.PayrollMonth == month && p.PayrollYear == year);
        }

        public bool Update(Payroll obj)
        {
            var existingPayroll = dbContext.Payrolls.FirstOrDefault(p => p.PayrollId == obj.PayrollId);
            if (existingPayroll == null)
                return false;
            existingPayroll.Status = obj.Status;
            existingPayroll.TotalAmount = obj.TotalAmount;

            return dbContext.SaveChanges() > 0;
        }
    }
}
