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


    #region Post
    public async Task<List<BlogPosts>?> GetPostsAsync()
    {
        return await _context.BlogPosts
            .Include(p=>p.BlogPostGroup)
            .ThenInclude(g=>g.BlogGroup)
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
            .FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<List<int>> GetBlogPostTagsByIdAsync(int postId)
    {
        return await _context.BlogPostTags
            .Include(t => t.BlogTag)
            .Where(t => t.PostId == postId).Select(t => t.TagId).ToListAsync();
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
}