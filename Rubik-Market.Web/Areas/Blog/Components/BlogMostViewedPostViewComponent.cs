using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts.Blog;

namespace Rubik_Market.Web.Areas.Blog.Components;

public class BlogMostViewedPostViewComponent(IBlogPanelServices blogPanelServices):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var list = await blogPanelServices.GetBlogMostViewedPost();
        return View("BlogMostViewedPost",list);
    }
}