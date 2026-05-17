using BLL.DTOs;
using BLL.Helpers;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        public static List<UserDTO> GetAll()
        {
            var data = DataAccessFactory.UserDataAccess().Get();
            return MapperConfig.GetMapper().Map<List<UserDTO>>(data);
        }

        public static UserDTO GetById(int id)
        {
            var data = DataAccessFactory.UserDataAccess().Get(id);
            if (data == null) return null;
            return MapperConfig.GetMapper().Map<UserDTO>(data);
        }

        public static UserDTO GetByUsername(string username)
        {
            var data = DataAccessFactory.UserDataAccess().GetByUsername(username);
            if (data == null) return null;
            return MapperConfig.GetMapper().Map<UserDTO>(data);
        }

        public static bool ToggleActive(int userId)
        {
            var user = DataAccessFactory.UserDataAccess().Get(userId);
            if (user == null)
                throw new Exception("User not found.");

            user.IsActive = !user.IsActive;

            if (!user.IsActive)
                DataAccessFactory.RefreshTokenDataAccess().RevokeByUserId(userId);

            return DataAccessFactory.UserDataAccess().Update(user);
        }
    }
}
