using Rubik_Market.Domain.ViewModels.AboutUs;
namespace Rubik_Market.Application.Services.Contracts;

public interface IAboutUsServices
{
    Task<AboutUsDescriptionViewModel?> GetAboutUsDescriptionAsync();
    Task<AddAboutUsDescriptionResult> AddAboutUsDescriptionAsync(AddAboutUsDescriptionViewModel model);
    Task<EditAboutUsDescriptionViewModel> GetAboutUsDescriptionForEditAsync();
    Task<EditAboutUsDescriptionResult> UpdateAboutUsDescriptionAsync(EditAboutUsDescriptionViewModel model);


    Task<List<TeamMembersViewModel?>> GetAllTeamMembersAsync();
    Task<AddTeamMemberResult> AddTeamMemberAsync(AddTeamMemberViewModel model);
    Task<EditTeamMemberViewModel> GetTeamMemberForEditAsync(int id);
    Task<EditTeamMemberResult> UpdateTeamMemberAsync(EditTeamMemberViewModel model);
    Task<DeleteTeamMemberResult> DeleteTeamMemberAsync(int id);
}