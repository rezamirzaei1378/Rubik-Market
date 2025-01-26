using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Components;

public class AboutTeamViewComponent(IAboutUsServices aboutUsServices):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await aboutUsServices.GetAllTeamMembersAsync();
        return View("AboutTeam",model);
    }
}