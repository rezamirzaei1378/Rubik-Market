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
        throw new NotImplementedException();
    }

    #endregion

    #region Tags
    public async Task<List<BlogTag>?> GetBlogTagListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateBlogTagAsync(BlogTag model)
    {
        throw new NotImplementedException();
    }

    public async Task<BlogTag> GetBlogTagByIdAsync(int groupId)
    {
        throw new NotImplementedException();
    }

    public void EditBlogTagAsync(BlogTag model)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Groups
    public async Task<List<BlogGroup>?> GetBlogGroupListAsync()
    {
        return await _context.BlogGroups.ToListAsync();
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