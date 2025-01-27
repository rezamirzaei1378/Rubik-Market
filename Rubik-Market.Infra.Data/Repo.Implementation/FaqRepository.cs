using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation;

public class FaqRepository : IFaqRepository
{
    #region Costructor

    private readonly RubikMarketDbContext _context;

    public FaqRepository(RubikMarketDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    public async Task<List<Faq>> GetAllFaqAsync()
    {
        return await _context.Faqs.ToListAsync();
    }

    public async Task CreateFaqAsync(Faq faq)
    {
        await _context.Faqs.AddAsync(faq);
    }

    public async Task<Faq?> GetFaqForEditAsync(int id)
    {
        return await _context.Faqs.Where(f=>f.isDelete == false).FirstOrDefaultAsync(f => f.ID == id);
    }

    public void UpdateFaq(Faq faq)
    {
        _context.Faqs.Update(faq);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion

}