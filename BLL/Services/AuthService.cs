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
    public class AuthService
    {
        public static TokenResponseDTO Login(LoginDTO obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.Username) || string.IsNullOrWhiteSpace(obj.Password))
                throw new Exception("Username and password are required.");

            var user = DataAccessFactory.UserDataAccess().GetByUsername(obj.Username);

            if (user == null || !user.IsActive)
                throw new Exception("Invalid username or password.");

            bool passwordValid = BCrypt.Net.BCrypt.Verify(obj.Password, user.PasswordHash);
            if (!passwordValid)
                throw new Exception("Invalid username or password.");

            user.LastLogin = DateTime.Now;
            DataAccessFactory.UserDataAccess().Update(user);

            DataAccessFactory.RefreshTokenDataAccess().RevokeByUserId(user.UserId);

            var accessToken = JWTHelper.GenerateAccessToken(user.UserId, user.Username, user.Role);
            var refreshToken = JWTHelper.GenerateRefreshToken();

            var tokenRecord = new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false
            };
            DataAccessFactory.RefreshTokenDataAccess().Create(tokenRecord);

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresInMinutes = 15,
                Role = user.Role,
                UserId = user.UserId,
                Username = user.Username
            };
        }

        public static TokenResponseDTO RefreshAccessToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new Exception("Refresh token is required.");

            var tokenRecord = DataAccessFactory.RefreshTokenDataAccess().GetByToken(refreshToken);

            if (tokenRecord == null)
                throw new Exception("Invalid or revoked refresh token.");

            if (tokenRecord.ExpiryDate < DateTime.Now)
            {
                tokenRecord.IsRevoked = true;
                DataAccessFactory.RefreshTokenDataAccess().Update(tokenRecord);
                throw new Exception("Refresh token has expired. Please log in again.");
            }

            var user = DataAccessFactory.UserDataAccess().Get(tokenRecord.UserId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found or inactive.");
            }

            tokenRecord.IsRevoked = true;
            DataAccessFactory.RefreshTokenDataAccess().Update(tokenRecord);

            string accessToken = JWTHelper.GenerateAccessToken(user.UserId, user.Username, user.Role);
            string newRefreshToken = JWTHelper.GenerateRefreshToken();

            var newTokenRecord = new RefreshToken
            {
                UserId = user.UserId,
                Token = newRefreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false
            };
            DataAccessFactory.RefreshTokenDataAccess().Create(newTokenRecord);

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                ExpiresInMinutes = 15,
                Role = user.Role,
                UserId = user.UserId,
                Username = user.Username
            };
        }

        public static bool Logout(int userId)
        {
            return DataAccessFactory.RefreshTokenDataAccess().RevokeByUserId(userId);
        }

        public static bool ChangePassword(ChangePasswordDTO obj)
        {
            var user = DataAccessFactory.UserDataAccess().Get(obj.UserId);
            if (user == null)
                throw new Exception("User not found.");

            bool currentValid = BCrypt.Net.BCrypt.Verify(obj.CurrentPassword, user.PasswordHash);
            if (!currentValid)
                throw new Exception("Current password is incorrect.");

            if (string.IsNullOrWhiteSpace(obj.NewPassword) || obj.NewPassword.Length < 6)
                throw new Exception("New password must be at least 6 characters.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(obj.NewPassword);
            return DataAccessFactory.UserDataAccess().Update(user);
        }

        public static bool AssignRole(AssignRoleDTO obj)
        {
            var validRoles = new[] { "Admin", "HR", "Employee" };
            if (Array.IndexOf(validRoles, obj.Role) < 0)
                throw new Exception("Invalid role. Must be Admin, HR, or Employee.");

            var user = DataAccessFactory.UserDataAccess().Get(obj.UserId);
            if (user == null)
                throw new Exception("User not found.");

            user.Role = obj.Role;
            return DataAccessFactory.UserDataAccess().Update(user);
        }

        public static bool CreateEmployeeUser(int employeeId, string firstName)
        {
            string username = (firstName.ToLower().Trim() + employeeId).Replace(" ", "");

            string defaultPassword = "EMP" + employeeId;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            var newUser = new User
            {
                Username     = username,
                PasswordHash = passwordHash,
                Role         = "Employee",
                IsActive     = true
            };
            return DataAccessFactory.UserDataAccess().Create(newUser);
        }
    }
}
