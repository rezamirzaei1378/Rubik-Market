using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Components;

public class AboutUsDescViewComponent(IAboutUsServices aboutUsServices):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await aboutUsServices.GetAboutUsDescriptionAsync();
        return View("AboutUsDesc",model);
    }
}