using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation;

public class ContactUsRepository : IContactUsRepository
{
    #region Constructor

    private readonly RubikMarketDbContext _context;

    public ContactUsRepository(RubikMarketDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods


    public async Task<List<ContactUs>?> GetContactUsListAsync()
    {
       return await _context.ContactUs.OrderByDescending(c => c.CreateDate).ToListAsync();
    }

    public async Task CreateContactUsAsync(ContactUs contactUs)
    {
        await _context.ContactUs.AddAsync(contactUs);
    }

    public async Task<ContactUs?> GetContactUsByIdAsync(int id)
    {
        return await _context.ContactUs.FirstOrDefaultAsync(c => c.ID == id);
    }

    public void UpdateContactUs(ContactUs contactUs)
    {
        _context.ContactUs.Update(contactUs);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}