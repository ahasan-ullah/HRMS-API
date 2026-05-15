using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRefreshTokenRepo : IRepo<RefreshToken,int,bool>
    {
        RefreshToken GetByToken(string token);
        bool RevokeByUserId(int userId);
    }
}
