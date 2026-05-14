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
    internal class BonusRepo : IBonusRepo
    {
        private readonly HRMSDbContext dbContext;
        public BonusRepo()
        {
            dbContext = new HRMSDbContext();
        }
        public bool Create(Bonus obj)
        {
            if(obj == null)
            {
                return false;
            }

            dbContext.Bonuses.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var bonus = dbContext.Bonuses.FirstOrDefault(b => b.BonusId == id);
            if (bonus == null) return false;

            dbContext.Bonuses.Remove(bonus);
            return dbContext.SaveChanges() > 0;
        }

        public List<Bonus> Get()
        {
            return dbContext.Bonuses.Include(b => b.Employee).ToList();
        }

        public Bonus Get(int id)
        {
            return dbContext.Bonuses.Include(b => b.Employee).FirstOrDefault(b => b.BonusId == id);
        }

        public List<Bonus> GetBonusesByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bonus obj)
        {
            var existingBonus = dbContext.Bonuses.FirstOrDefault(b => b.BonusId == obj.BonusId);
            if (existingBonus == null) return false;

            existingBonus.BonusType = obj.BonusType;
            existingBonus.Amount = obj.Amount;
            existingBonus.BonusDate = obj.BonusDate;
            existingBonus.Reason = obj.Reason;

            return dbContext.SaveChanges() > 0;
        }
    }
}
