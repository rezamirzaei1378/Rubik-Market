using Rubik_Market.Domain.Models;

namespace Rubik_Market.Domain.Repo.Contracts;

public interface IAboutUsRepository
{
    Task<AboutUsDescription?> GetAboutUsDescriptionAsync();
    Task AddAboutUsDescriptionAsync(AboutUsDescription model);
    public void UpdateAboutUsDescription(AboutUsDescription model);
    Task AboutSaveAsync();

    Task<List<AboutTeam>> GetTeamMembersDetailAsync();
    Task AddTeamMemberAsync(AboutTeam model);
    Task<AboutTeam> GetTeamMemberAsync(int id);
    public void UpdateTeamMember(AboutTeam model);
}