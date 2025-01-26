using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Areas.UserPanel.Components;

public class UserPanelSideBarViewComponent(IUserProfileServices userProfileServices) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var menuModel = await userProfileServices.IsProfileForAddOrEdit(id);
        return View("UserPanelSideBar", menuModel);
    }
}