using Rubik_Market.Domain.Models.Blog;

namespace Rubik_Market.Domain.Repo.Contracts
{
    public interface IBlogRepository
    {
        #region Post

        Task<List<BlogPosts>?> GetPostsAsync();

        #endregion

        #region Tags

        Task<List<BlogTag>?> GetBlogTagListAsync();
        Task CreateBlogTagAsync(BlogTag model);
        Task<BlogTag?> GetBlogTagByIdAsync(int tagId);
        public void EditBlogTagAsync(BlogTag model);

        #endregion

        #region Groups

        Task<List<BlogGroup>?> GetBlogGroupListAsync();
        Task CreateBlogGroupAsync(BlogGroup model);
        Task<BlogGroup?> GetBlogGroupByIdAsync(int groupId);
        public void EditBlogGroupAsync(BlogGroup model);

        #endregion

        #region Shared

        Task SaveAsync();

        #endregion
    }
}
