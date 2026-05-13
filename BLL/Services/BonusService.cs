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
    public class BonusService
    {
        public static List<BonusDTO> Get()
        {
            var data = DataAccessFactory.BonusDataAccess().Get();
            return MapperConfig.GetMapper().Map<List<BonusDTO>>(data);
        }
        public static BonusDTO Get(int id)
        {
            var data = DataAccessFactory.BonusDataAccess().Get(id);
            if (data == null) return null;
            return MapperConfig.GetMapper().Map<BonusDTO>(data);
        }
        public static List<BonusDTO> GetByEmployeeId(int employeeId)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(employeeId);
            if (employee == null)
                throw new Exception("Employee not found");
            var data = DataAccessFactory.BonusDataAccess().GetBonusesByEmployeeId(employeeId);
            return MapperConfig.GetMapper().Map<List<BonusDTO>>(data);
        }
        public static bool Create(BonusDTO bonus)
        {
            var employee = DataAccessFactory.EmployeeDataAccess().Get(bonus.EmployeeId);
            if (employee == null)
                throw new Exception("Employee not found");
            if (bonus.Amount <= 0)
                throw new Exception("Bonus amount must be greater than zero");

            var data = MapperConfig.GetMapper().Map<Bonus>(bonus);
            return DataAccessFactory.BonusDataAccess().Create(data);
        }
        public static bool Update(BonusDTO obj)
        {
            var existing = DataAccessFactory.BonusDataAccess().Get(obj.BonusId);
            if (existing == null)
                throw new Exception("Bonus not found.");

            if (obj.Amount <= 0)
                throw new Exception("Bonus amount must be greater than zero.");

            var bonus = MapperConfig.GetMapper().Map<Bonus>(obj);
            return DataAccessFactory.BonusDataAccess().Update(bonus);
        }
        public static bool Delete(int id)
        {
            var existing = DataAccessFactory.BonusDataAccess().Get(id);
            if (existing == null)
                throw new Exception("Bonus not found.");

            return DataAccessFactory.BonusDataAccess().Delete(id);
        }
    }
}
