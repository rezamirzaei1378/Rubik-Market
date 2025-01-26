using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation
{
    #region UserRepository

    public class UserRepository : IUserRepository
    {
        #region Constructor

        private readonly RubikMarketDbContext _context;

        public UserRepository(RubikMarketDbContext context)
        {
            _context = context;
        }

        #endregion

        #region ForAdmin
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Where(u=>u.isDelete == false).ToListAsync();
        }

        public async Task<List<User>> GetAllDeletedUsersAsync()
        {
            return await _context.Users.Where(u => u.isDelete == true).ToListAsync();
        }

        #endregion

        #region ForUserPanel

        //Empty

        #endregion

        #region ForSite

        public async Task<bool> IsUserExistByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user != null)
                return true;
            return false;
        }
        public async Task<User?> GetUserByConfirmCodeAsync(string confirmCode)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ConfirmCode == confirmCode);
        }
        #endregion

        #region Shared

        public async Task AddUserAsync(User user)
        {
            await _context.AddAsync(user);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
                user.isDelete = true;
            DeleteUser(user);
        }

        public void DeleteUser(User? user)
        {
            UpdateUser(user!);
        }

        public async Task<User?> GetUserByIdAsync(int? id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ID == id && u.isDelete == false);
        }
        public bool IsUserExistByEmailAsync(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        public async Task<User?> GetUserByEmailAsync(string? email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        #endregion
    }

    #endregion

    #region UserProfileRepository

    public class UserProfileRepository(RubikMarketDbContext context) : IUserProfileRepository
    {

        #region ForAdmin

        //Empty

        #endregion

        #region ForUserPanel

        //Empty

        #endregion

        #region ForSite

        //Empty

        #endregion

        #region Shared

        public async Task<UserProfileInfo?> GetUserProfileAsync(int? userId)
        {
            return await context.UserProfileInfo.FirstOrDefaultAsync(p => p.UserId == userId &&p.isDelete == false);
        }

        public async Task AddUserProfileAsync(UserProfileInfo userProfile)
        {
            await context.UserProfileInfo.AddAsync(userProfile);
        }

        public void UpdateUserProfileAsync(UserProfileInfo userProfile)
        {
            context.UserProfileInfo.Update(userProfile);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        #endregion

    }

    #endregion

}
