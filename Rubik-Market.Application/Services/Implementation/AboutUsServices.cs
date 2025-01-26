using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Application.Tools;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.AboutUs;

namespace Rubik_Market.Application.Services.Implementation;

public class AboutUsServices : IAboutUsServices
{
    #region Constroctor

    private readonly IAboutUsRepository _aboutUsRepository;

    public AboutUsServices(IAboutUsRepository aboutUsRepository)
    {
        _aboutUsRepository = aboutUsRepository;
    }

    #endregion

    #region Shared

    public async Task<AboutUsDescriptionViewModel?> GetAboutUsDescriptionAsync()
    {
        var aboutUsDesc = await _aboutUsRepository.GetAboutUsDescriptionAsync();
        if (aboutUsDesc != null)
        {
            return new AboutUsDescriptionViewModel()
            {
                ID = aboutUsDesc.ID,
                AboutUsText = aboutUsDesc.AboutUsText,
                AboutUsImageName = aboutUsDesc.AboutUsImageName
            };
        }
        else
        {
            return null;
        }

    }


    public async Task<List<TeamMembersViewModel?>> GetAllTeamMembersAsync()
    {
        var teamMember = await _aboutUsRepository.GetTeamMembersDetailAsync();
        if (teamMember != null)
        {
            var model = teamMember.Select(t => new TeamMembersViewModel()
            {
                ID = t.ID,
                TemaMemberName = t.Name,
                TemaMemberJobTitle = t.JobTitle,
                TemaMemberShortDescription = t.ShortDescription,
                TeamMemberImgName = t.TeamMemberImgName,
                TwitterUrl = t.TwitterUrl,
                RedditUrl = t.RedditUrl,
                GithubUrl = t.GithubUrl,
                LinkedInUrl = t.LinkedInUrl,
                ZLinkUrl = t.ZLinkUrl,
                CreateDate = t.CreateDate,
                isDelete = t.isDelete
            }).ToList();

            return model;
        }
        else
        {
            return null;
        }
    }



    #endregion

    #region ForAdmin


    public async Task<AddAboutUsDescriptionResult> AddAboutUsDescriptionAsync(AddAboutUsDescriptionViewModel model)
    {
        try
        {
            var aboutUsDescription = new AboutUsDescription();
            aboutUsDescription.AboutUsText = model.AboutUsText;
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.AboutMeImage, thumbPath: SiteTools.AboutMeImageThumbPath);
                aboutUsDescription.AboutUsImageName = imageName;
            }
            else
            {
                aboutUsDescription.AboutUsImageName = "Default.png";
            }

            await _aboutUsRepository.AddAboutUsDescriptionAsync(aboutUsDescription);
            await _aboutUsRepository.AboutSaveAsync();
            return AddAboutUsDescriptionResult.Success;
        }
        catch
        {
            return AddAboutUsDescriptionResult.Error;
        }
    }

    public async Task<EditAboutUsDescriptionViewModel> GetAboutUsDescriptionForEditAsync()
    {
        var aboutUsDesc = await _aboutUsRepository.GetAboutUsDescriptionAsync();
        if (aboutUsDesc != null)
        {
            return new EditAboutUsDescriptionViewModel()
            {
                Id = aboutUsDesc.ID,
                AboutUsText = aboutUsDesc.AboutUsText,
                AboutUsImageName = aboutUsDesc.AboutUsImageName
            };
        }

        return null;
    }

    public async Task<EditAboutUsDescriptionResult> UpdateAboutUsDescriptionAsync(EditAboutUsDescriptionViewModel model)
    {
        var aboutUsDescription = await _aboutUsRepository.GetAboutUsDescriptionAsync();
        if (aboutUsDescription == null)
        {
            return EditAboutUsDescriptionResult.Error;
        }

        try
        {

            aboutUsDescription.ID = model.Id;
            aboutUsDescription.AboutUsText = model.AboutUsText;
            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.AboutMeImage, thumbPath: SiteTools.AboutMeImageThumbPath, deletefileName: model.AboutUsImageName);
                aboutUsDescription.AboutUsImageName = imageName;
            }
            else
            {
                aboutUsDescription.AboutUsImageName = aboutUsDescription.AboutUsImageName;
            }

            _aboutUsRepository.UpdateAboutUsDescription(aboutUsDescription);
            await _aboutUsRepository.AboutSaveAsync();
            return EditAboutUsDescriptionResult.Success;
        }
        catch
        {
            return EditAboutUsDescriptionResult.Error;
        }
    }


    public async Task<AddTeamMemberResult> AddTeamMemberAsync(AddTeamMemberViewModel model)
    {
        try
        {
            var teamMember = new AboutTeam
            {
                CreateDate = DateTime.Now,
                isDelete = false,
                Name = model.TemaMemberName,
                JobTitle = model.TemaMemberJobTitle,
                ShortDescription = model.TemaMemberShortDescription,
                TwitterUrl = model.TwitterUrl,
                LinkedInUrl = model.LinkedInUrl,
                RedditUrl = model.RedditUrl,
                GithubUrl = model.GithubUrl,
                ZLinkUrl = model.ZLinkUrl,
            };

            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.TeamMemberImage, thumbPath: SiteTools.TeamMemberImageThumbPath);
                teamMember.TeamMemberImgName = imageName;
            }
            else
            {
                teamMember.TeamMemberImgName = "Default.png";
            }

            await _aboutUsRepository.AddTeamMemberAsync(teamMember);
            await _aboutUsRepository.AboutSaveAsync();
            return AddTeamMemberResult.Success;
        }
        catch
        {
            return AddTeamMemberResult.Error;
        }
    }

    public async Task<EditTeamMemberViewModel> GetTeamMemberForEditAsync(int id)
    {
        var member = await _aboutUsRepository.GetTeamMemberAsync(id);
        if (member != null)
        {
            return new EditTeamMemberViewModel
            {
                Id = member.ID,
                TemaMemberName = member.Name,
                TemaMemberJobTitle = member.JobTitle,
                TemaMemberShortDescription = member.ShortDescription,
                TeamMemberImgName = member.TeamMemberImgName,
                TwitterUrl = member.TwitterUrl,
                LinkedInUrl = member.LinkedInUrl,
                RedditUrl = member.RedditUrl,
                GithubUrl = member.GithubUrl,
                ZLinkUrl = member.ZLinkUrl
            };
        }
        else
        {
            return null;
        }
    }

    public async Task<EditTeamMemberResult> UpdateTeamMemberAsync(EditTeamMemberViewModel model)
    {
        var teamMember = await _aboutUsRepository.GetTeamMemberAsync(model.Id);
        if (teamMember == null)
        {
            return EditTeamMemberResult.Error;
        }

        try
        {
            teamMember.Name = model.TemaMemberName;
            teamMember.JobTitle = model.TemaMemberJobTitle;
            teamMember.ShortDescription = model.TemaMemberShortDescription;
            teamMember.LinkedInUrl = model.LinkedInUrl;
            teamMember.ZLinkUrl = model.ZLinkUrl;
            teamMember.GithubUrl = model.GithubUrl;
            teamMember.TwitterUrl = model.TwitterUrl;
            teamMember.RedditUrl = model.RedditUrl;

            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.AboutMeImage, thumbPath: SiteTools.AboutMeImageThumbPath, deletefileName: teamMember.TeamMemberImgName);
                teamMember.TeamMemberImgName = imageName;
            }
            else
            {
                teamMember.TeamMemberImgName = teamMember.TeamMemberImgName;
            }

            _aboutUsRepository.UpdateTeamMember(teamMember);
            await _aboutUsRepository.AboutSaveAsync();

            return EditTeamMemberResult.Success;
        }
        catch
        {
            return EditTeamMemberResult.Error;
        }
    }

    public async Task<DeleteTeamMemberResult> DeleteTeamMemberAsync(int id)
    {
        var teamMember = await _aboutUsRepository.GetTeamMemberAsync(id);
        if (teamMember == null)
        {
            return DeleteTeamMemberResult.Error;
        }

        try
        {
            teamMember.isDelete = true;
            _aboutUsRepository.UpdateTeamMember(teamMember);
            await _aboutUsRepository.AboutSaveAsync();
            return DeleteTeamMemberResult.Success;
        }
        catch
        {
            return DeleteTeamMemberResult.Error;
        }
    }

    #endregion
}