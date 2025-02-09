using Rubik_Market.Domain.Models.Blog;

namespace Rubik_Market.Domain.Repo.Contracts
{
    public interface IBlogRepository
    {
        #region AdminSide

        #region Post

        Task<List<BlogPosts>?> GetPostsAsync();
        Task AddBlogPost(BlogPosts blogPost);
        Task<BlogPosts> GetBlogPostByIdAsync(int id);
        Task<List<int>> GetBlogPostTagsByIdAsync(int postId);
        public void UpdateBlogPost(BlogPosts blogPost);
        Task<List<BlogPostTags>> GetTagsListToRemove(int postId, List<int>? postTagListToRemove);
        Task<List<BlogPostTags>> GetTagsListToNotRemove(int postId, List<int>? postTagListToRemove);
        public void BlogPostTagToRemove(List<BlogPostTags> blogTags);
        public void DeletePostTag(BlogPosts blogPosts, List<BlogPostTags> blogPostTags, BlogPostGroup postGroup);
        Task<List<BlogPostTags>?> GetBlogPostTagsListAsync(int postId);
        Task<BlogPostGroup?> GetBlogPostGroupAsync(int postId);
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

        #endregion



        #region UserSide



        #endregion
    }
}
