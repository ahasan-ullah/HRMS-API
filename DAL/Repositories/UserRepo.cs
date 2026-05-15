using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class UserRepo:IUserRepo
    {
        private readonly HRMSDbContext dbContext;
        public UserRepo()
        {
            dbContext = new HRMSDbContext();
        }

        public bool Create(User obj)
        {
            if(dbContext.Users.Any(u => u.Username == obj.Username))
            {
                throw new Exception("Username already exists.");
            }
            if(obj == null)
            {
                return false;
            }
            dbContext.Users.Add(obj);
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            return dbContext.SaveChanges() > 0;
        }

        public List<User> Get()
        {
            return dbContext.Users.ToList();
        }

        public User Get(int id)
        {
            return dbContext.Users.FirstOrDefault(u => u.UserId == id);
        }

        public User GetByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool Update(User obj)
        {
            var existingUser = dbContext.Users.FirstOrDefault(u => u.UserId == obj.UserId);
            if (existingUser == null)
            {
                return false;
            }
            existingUser.Role = obj.Role;
            existingUser.IsActive = obj.IsActive;
            existingUser.LastLogin = obj.LastLogin;
            return dbContext.SaveChanges() > 0;
        }
    }
}
