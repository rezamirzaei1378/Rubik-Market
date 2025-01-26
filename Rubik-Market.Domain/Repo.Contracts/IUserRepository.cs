using Rubik_Market.Domain.Models;

namespace Rubik_Market.Domain.Repo.Contracts;

public interface IUserRepository
{
    #region Admin

    Task<List<User>> GetAllUsersAsync();
    Task<List<User>> GetAllDeletedUsersAsync();

    #endregion

    #region ForUserPanel

    //Empty

    #endregion

    #region ForSite

    Task<bool> IsUserExistByIdAsync(int id);
    Task<User?> GetUserByConfirmCodeAsync(string confirmCode);

    #endregion

    #region Shared

    Task AddUserAsync(User user);
    Task SaveAsync();
    public void UpdateUser(User user);
    Task DeleteUserAsync(int id);
    public void DeleteUser(User? user);
    Task<User?> GetUserByIdAsync(int? id);
    public bool IsUserExistByEmailAsync(string email);
    Task<User?> GetUserByEmailAsync(string? email);

    #endregion

}

public interface IUserProfileRepository
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

    Task<UserProfileInfo?> GetUserProfileAsync(int? userId);
    Task AddUserProfileAsync(UserProfileInfo userProfile);
    public void UpdateUserProfileAsync(UserProfileInfo userProfile);
    Task SaveAsync();

    #endregion
}