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
    internal class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly HRMSDbContext dbContext;
        public RefreshTokenRepo()
        {
            dbContext = new HRMSDbContext();
        }
        public bool Create(RefreshToken obj)
        {
            if (obj == null)
            {
                return false;
            }
            dbContext.RefreshTokens.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var token = dbContext.RefreshTokens.FirstOrDefault(r => r.TokenId == id);
            if (token == null)
            {
                return false;
            }
            dbContext.RefreshTokens.Remove(token);
            return dbContext.SaveChanges() > 0;
        }

        public List<RefreshToken> Get()
        {
            return dbContext.RefreshTokens.ToList();
        }

        public RefreshToken Get(int id)
        {
            return dbContext.RefreshTokens.FirstOrDefault(r => r.TokenId == id);
        }

        public RefreshToken GetByToken(string token)
        {
            return dbContext.RefreshTokens.FirstOrDefault(r => r.Token == token);
        }

        public bool RevokeByUserId(int userId)
        {
            var tokens = dbContext.RefreshTokens.Where(r => r.UserId == userId && !r.IsRevoked).ToList();
            if (tokens.Count == 0)
            {
                return false;
            }
            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }
            return dbContext.SaveChanges() > 0;
        }

        public bool Update(RefreshToken obj)
        {
            var existing = dbContext.RefreshTokens.FirstOrDefault(r => r.TokenId == obj.TokenId);
            if (existing == null)
            {
                return false;
            }
            existing.IsRevoked = obj.IsRevoked;
            return dbContext.SaveChanges() > 0;
        }
    }
}
