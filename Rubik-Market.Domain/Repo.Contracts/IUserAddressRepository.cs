using Rubik_Market.Domain.Models;

namespace Rubik_Market.Domain.Repo.Contracts;

public interface IUserAddressRepository
{
    Task<List<Address?>> GetUserAddressesAsync(int id);
    Task<List<Address?>> GetAllUserAddressesAsync();
    Task AddUserAddressAsync(Address address);
    Task<Address?> GetUserAddressForEditAsync(int id);
    Task<Address?> GetUserCurrentAddressAsync(int id);
    void UpdateUserAddress(Address address);
    Task SaveAsync();
}