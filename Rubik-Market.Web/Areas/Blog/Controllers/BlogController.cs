using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts.Blog;

namespace Rubik_Market.Web.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class BlogController : Controller
    {
        #region Constructor

        private readonly IBlogPanelServices _blogPanelServices;

        public BlogController(IBlogPanelServices blogPanelServices)
        {
            _blogPanelServices = blogPanelServices;
        }

        #endregion


        [HttpGet("Blog")]
        public async Task<IActionResult> BlogLastPostList()
        {
            var model = await _blogPanelServices.GetLastBlogPostAsync();
            return View(model);
        }

        public async Task<IActionResult> BlogPostList(string? q)
        {
            return View();
        }

        [HttpGet("Single-BlogPost")]
        public async Task<IActionResult> SingleBlogPost(int postId)
        {
            await _blogPanelServices.AddBlogPostViewAsync(postId);
            var model = await _blogPanelServices.GetSingleBlogPostAsync(postId);
            return View(model);
        }

    }
}
