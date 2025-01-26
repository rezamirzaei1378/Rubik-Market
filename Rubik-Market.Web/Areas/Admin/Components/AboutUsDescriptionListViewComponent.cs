using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Areas.Admin.Components;

public class AboutUsDescriptionListViewComponent(IAboutUsServices aboutUsServices):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await aboutUsServices.GetAboutUsDescriptionAsync();
        return View("AboutUsDescriptionList",model);
    }
}