using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;

namespace DAL.Repositories
{
    internal class SalaryRevisionRepo: ISalaryRevisionRepo
    {
        private readonly HRMSDbContext dbContext;

        public SalaryRevisionRepo()
        {
            dbContext = new HRMSDbContext();
        }

        public bool Create(SalaryRevision obj)
        {
            if (obj == null) return false;

            dbContext.SalaryRevisions.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public List<SalaryRevision> Get()
        {
            return dbContext.SalaryRevisions.Include(r=>r.Employee).Include(r=>r.RevisedByUser).ToList();
        }

        public SalaryRevision Get(int id)
        {
            return dbContext.SalaryRevisions.Include(r => r.Employee).Include(r => r.RevisedByUser).FirstOrDefault(r => r.RevisionId == id);
        }

        public List<SalaryRevision> GetSalaryRevisionsByEmployeeId(int employeeId)
        {
            return dbContext.SalaryRevisions.Include(r=> r.Employee)
                .Include(r => r.RevisedByUser)
                .Where(r => r.EmployeeId == employeeId)
                .ToList();
        }

        public bool Update(SalaryRevision obj)
        {
            return false;
        }
    }
}
