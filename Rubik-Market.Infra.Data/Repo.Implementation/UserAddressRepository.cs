using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation;

public class UserAddressRepository : IUserAddressRepository
{
    #region Constructor

    private readonly RubikMarketDbContext _context;

    public UserAddressRepository(RubikMarketDbContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<List<Address?>> GetUserAddressesAsync(int userId)
    {
        return await _context.Addresses.Where(a => a.UserId == userId && !a.isDelete).ToListAsync();
    }

    public async Task<List<Address?>> GetAllUserAddressesAsync()
    {
        return await _context.Addresses.ToListAsync();
    }

    public async Task AddUserAddressAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
    }

    public async Task<Address?> GetUserAddressForEditAsync(int addressId)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.ID == addressId);
    }

    public async Task<Address?> GetUserCurrentAddressAsync(int id)
    {

        return await _context.Addresses.Where(a =>
            a.UserId == id && a.CurrentAddress == true && a.isDelete == false).FirstOrDefaultAsync();
    }

    public void UpdateUserAddress(Address address)
    {
        _context.Addresses.Update(address);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}