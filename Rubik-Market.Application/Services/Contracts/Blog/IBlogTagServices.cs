using Rubik_Market.Domain.ViewModels.Blog.BlogTags;

namespace Rubik_Market.Application.Services.Contracts.Blog;

public interface IBlogTagServices
{
    Task<List<BlogTagsViewModel>?> GetBlogTagsAsync();
    Task<AddBlogTagResult> AddBlogTagAsync(AddBlogTagViewModel blogTag);
    Task<EditBlogTagViewModel?> GetBlogTagByIdAsync(int  blogTagId);
    Task<EditBlogTagResult> EditBlogTagAsync(EditBlogTagViewModel blogTag);
}