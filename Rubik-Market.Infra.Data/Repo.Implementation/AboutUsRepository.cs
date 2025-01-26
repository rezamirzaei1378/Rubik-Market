using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation;

public class AboutUsRepository : IAboutUsRepository
{
    #region Constructor

    private readonly RubikMarketDbContext _context;

    public AboutUsRepository(RubikMarketDbContext context)
    {
        _context = context;
    }

    #endregion
    #region AboutUsDescription

    public async Task<AboutUsDescription?> GetAboutUsDescriptionAsync()
    {
        return await _context.AboutUsDescription.FirstOrDefaultAsync(i => i.isDelete == false);
    }

    public async Task AddAboutUsDescriptionAsync(AboutUsDescription model)
    {
        await _context.AboutUsDescription.AddAsync(model);
    }

    public void UpdateAboutUsDescription(AboutUsDescription model)
    {
        _context.AboutUsDescription.Update(model);
    }

    public void DeleteAboutUsDescription(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAboutUsDescription(AboutUsDescription model)
    {
        throw new NotImplementedException();
    }

    public async Task AboutSaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion


    public async Task<List<AboutTeam>> GetTeamMembersDetailAsync()
    {
        return await _context.AboutTeam.Where(t=>t.isDelete == false).ToListAsync();
    }

    public async Task AddTeamMemberAsync(AboutTeam model)
    {
        await _context.AboutTeam.AddAsync(model);
    }

    public async Task<AboutTeam> GetTeamMemberAsync(int id)
    {
        return await _context.AboutTeam.FirstOrDefaultAsync(m => m.ID == id);
    }

    public void UpdateTeamMember(AboutTeam model)
    {
        _context.AboutTeam.Update(model);
    }
}