using Rubik_Market.Domain.Models;

namespace Rubik_Market.Domain.Repo.Contracts;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    User GetUserById(int id);
    Task AddUserAsync(User user);
    public void UpdateUser(User user);
    public void DeleteUser(int id);
    public void DeleteUser(User user);
    Task SaveAsync();
    public bool IsUserExistByEmailAsync(string email);
    public bool IsUserExistByIdAsync(int id);
    User GetUserByConfirmCode(string confirmCode);
    public User GetUserByEmail (string email);

}