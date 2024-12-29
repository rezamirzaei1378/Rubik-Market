using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation
{
    #region UserRepository


    public class UserRepository(RubikMarketDbContext context) : IUserRepository
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(u => u.ID == id);
        }

        public async Task AddUserAsync(User user)
        {
            await context.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            DeleteUser(user);
        }

        public void DeleteUser(User user)
        {
            UpdateUser(user);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public bool IsUserExistByEmailAsync(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public User GetUserByConfirmCode(string confirmCode)
        {
            return context.Users.FirstOrDefault(u => u.ConfirmCode == confirmCode);
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool IsUserExistByIdAsync(int id)
        {
            return context.Users.Any(u => u.ID == id);
        }
    }
    #endregion

    #region UserPersonalInfoRepository

    public class UserPersonalInfoRepository(RubikMarketDbContext context): IUserPersonalInfoRepository
    {
        public async Task<List<UserProfileInfo>> GetAllUserPersonalInfo()
        {
            return await context.UserProfileInfo.ToListAsync();
        }

        public UserProfileInfo GetUserPersonalInfo(int id)
        {
            return context.UserProfileInfo.FirstOrDefault(u=>u.UserId == id);
        }

        public async Task AddUserPersonalInfo(UserProfileInfo userPersonalInfo)
        {
            await context.UserProfileInfo.AddAsync(userPersonalInfo);
        }

        public void UpdateUserPersonalInfo(UserProfileInfo userProfileInfo)
        {
            context.UserProfileInfo.Update(userProfileInfo);
        }

        public void DeleteUserPersonalInfo(int id)
        {
            var userPersonalInfo = GetUserPersonalInfo(id);
            DeleteUserPersonalInfo(userPersonalInfo);
        }

        public void DeleteUserPersonalInfo(UserProfileInfo userProfileInfo)
        {
            UpdateUserPersonalInfo(userProfileInfo);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public bool IsUserProfileExist(int id)
        {
            return context.UserProfileInfo.Any(u => u.UserId == id);
        }
    }

    #endregion

}
