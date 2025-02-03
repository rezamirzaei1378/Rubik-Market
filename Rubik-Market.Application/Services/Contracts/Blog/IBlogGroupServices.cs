using Rubik_Market.Domain.ViewModels.Blog.BlogGroups;

namespace Rubik_Market.Application.Services.Contracts.Blog;

public interface IBlogGroupServices
{
    Task<List<BlogGroupViewModel>?> GetBlogGroupsAsync();
    Task<AddBlogGroupResult> AddBlogGroupAsync(AddBlogGroupViewModel model);
    Task<EditBlogGroupViewModel?> GetBlogGroupByIdAsync(int id);
    Task<EditBlogGroupResult> EditBlogGroupAsync(EditBlogGroupViewModel model);
}