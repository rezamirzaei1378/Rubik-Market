using Microsoft.EntityFrameworkCore;
using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Infra.Data.Repo.Implementation;

public class BlogRepository : IBlogRepository
{
    #region Constructor

    private readonly RubikMarketDbContext _context;

    public BlogRepository(RubikMarketDbContext context)
    {
        _context = context;
    }

    #endregion


    #region AdminSide

    #region Post
    public async Task<List<BlogPosts>?> GetPostsAsync()
    {
        return await _context.BlogPosts
            .Include(p=>p.BlogPostGroup)
            .ThenInclude(g=>g.BlogGroup)
            .Where(p=>p.isDelete == false)
            .OrderByDescending(p=>p.CreateDate).ToListAsync();
    }

    public async Task AddBlogPost(BlogPosts blogPost)
    {
        await _context.BlogPosts.AddAsync(blogPost);
    }

    public async Task<BlogPosts> GetBlogPostByIdAsync(int id)
    {
        return await _context.BlogPosts.Include(p => p.BlogPostGroup)
            .ThenInclude(g => g.BlogGroup)
            .Include(p => p.BlogPostTags)
            .ThenInclude(t => t.BlogTag)
            .Where(p => p.isDelete == false)
            .FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<List<int>> GetBlogPostTagsByIdAsync(int postId)
    {
        return await _context.BlogPostTags
            .Include(t => t.BlogTag)
            .Where(t => t.PostId == postId && t.isDelete == false).Select(t => t.TagId).ToListAsync();
    }

    public void UpdateBlogPost(BlogPosts blogPost)
    {
        _context.BlogPosts.Update(blogPost);
    }

    public async Task<List<BlogPostTags>> GetTagsListToRemove(int postId, List<int>? postTagListToRemove)
    {
        return await _context.BlogPostTags
            .Where(t => t.PostId == postId && postTagListToRemove.Contains(t.TagId)).ToListAsync();
    }

    public async Task<List<BlogPostTags>> GetTagsListToNotRemove(int postId, List<int>? postTagListToRemove)
    {
        return await _context.BlogPostTags
            .Where(t => t.PostId == postId && !postTagListToRemove.Contains(t.TagId)).ToListAsync();
    }

    public void BlogPostTagToRemove(List<BlogPostTags> blogTags)
    { 
        _context.BlogPostTags.RemoveRange(blogTags);
    }

    public void DeletePostTag(BlogPosts blogPosts, List<BlogPostTags> blogPostTags, BlogPostGroup postGroup)
    {
        _context.BlogPosts.Update(blogPosts);
        _context.BlogPostTags.UpdateRange(blogPostTags);
        _context.BlogPostGroups.Update(postGroup);
    }

    public async Task<List<BlogPostTags>?> GetBlogPostTagsListAsync(int postId)
    {
        return await _context.BlogPostTags.Where(t => t.PostId == postId && t.isDelete == false).ToListAsync();
    }

    public async Task<BlogPostGroup?> GetBlogPostGroupAsync(int postId)
    {
        return await _context.BlogPostGroups.FirstOrDefaultAsync(g => g.PostId == postId && g.isDelete == false);
    }

    #endregion

    #region Tags
    public async Task<List<BlogTag>?> GetBlogTagListAsync()
    {
        return await _context.BlogTagLists.OrderByDescending(t=>t.CreateDate).ToListAsync();
    }

    public async Task CreateBlogTagAsync(BlogTag model)
    {
        await _context.BlogTagLists.AddAsync(model);
    }

    public async Task<BlogTag?> GetBlogTagByIdAsync(int tagId)
    {
        return await _context.BlogTagLists.FirstOrDefaultAsync(t => t.ID == tagId);
    }

    public void EditBlogTagAsync(BlogTag model)
    {
        _context.BlogTagLists.Update(model);
    }
    #endregion

    #region Groups
    public async Task<List<BlogGroup>?> GetBlogGroupListAsync()
    {
        return await _context.BlogGroups.OrderByDescending(g => g.CreateDate).ToListAsync();
    }

    public async Task CreateBlogGroupAsync(BlogGroup model)
    {
        await _context.BlogGroups.AddAsync(model);
    }

    public async Task<BlogGroup?> GetBlogGroupByIdAsync(int groupId)
    {
        return await _context.BlogGroups.FirstOrDefaultAsync(g => g.ID == groupId);
    }

    public void EditBlogGroupAsync(BlogGroup model)
    {
        _context.BlogGroups.Update(model);
    }
    #endregion

    #region Shared
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion

    #endregion

    #region BlogPanel

    public async Task<List<BlogPosts>?> GetLastBlogPostsListAsync()
    {
        return await _context.BlogPosts.OrderByDescending(p => p.CreateDate)
            .Where(p=>p.isDelete == false).Take(8)
            .Include(p=>p.BlogPostGroup)
            .Include(p => p.BlogPostTags)
            .ToListAsync();
    }

    public async Task<BlogPosts?> GetSingleBlogPostAsync(int postId)
    {
        return await _context.BlogPosts.Where(p => p.ID == postId)
            .Include(p => p.BlogPostGroup)
            .ThenInclude(g=>g.BlogGroup)
            .Include(p => p.BlogPostTags)
            .ThenInclude(t=>t.BlogTag)
            .Include(p=>p.Views)
            .FirstOrDefaultAsync();
    }

    public async Task AddBlogPostViewAsync(BlogPostView postView)
    {
        await _context.BolgPostViews.AddAsync(postView);
    }

    public async Task<List<BlogPosts>?> GetBlogMostViewedPostAsync()
    {
        return await _context.BlogPosts.Where(p=>p.isDelete == false)
            .Include(p => p.BlogPostGroup)
            .ThenInclude(g => g.BlogGroup)
            .OrderByDescending(p => p.Views.Count).Take(4).ToListAsync();
    }

    #endregion
}