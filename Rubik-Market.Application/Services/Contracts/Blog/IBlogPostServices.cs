using Rubik_Market.Domain.ViewModels.Blog.BlogPost;

namespace Rubik_Market.Application.Services.Contracts.Blog
{
    public interface IBlogPostServices
    {
        Task<List<BlogPostViewModel>?> GetPostsAsync();
        Task<CreateBlogPostResult> CreateBlogPost(CreateBlogPostViewModel model);
        Task<CreateBlogPostViewModel> GetBlogPostTagsAndGroupsAsync();
        Task<EditBlogPostViewModel?> GetBlogForEditAsync(int postId);
        Task<EditBlogPostResult> EditBlogPostAsync(EditBlogPostViewModel model);
        Task<DeleteBLogPostResult> DeleteBlogPostAsync(int postId);
    }
}
