using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.ViewModels.Blog.BlogArea;

namespace Rubik_Market.Application.Services.Contracts.Blog;

public interface IBlogPanelServices
{
    Task<List<BlogPostCardViewModel>?> GetLastBlogPostAsync();
    Task<BlogPostDetailsViewModel?> GetSingleBlogPostAsync(int postId);
    Task AddBlogPostViewAsync(int postId);
    Task<List<BlogPostMostViewedViewModel>?> GetBlogMostViewedPost();
}