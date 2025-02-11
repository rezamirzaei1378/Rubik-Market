using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Blog.BlogArea;

namespace Rubik_Market.Application.Services.Implementation.Blog;

public class BlogPanelServices : IBlogPanelServices
{
    #region Constructor

    private readonly IBlogRepository _blogRepository;

    public BlogPanelServices(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    #endregion
    public async Task<List<BlogPostCardViewModel>?> GetLastBlogPostAsync()
    {
        var blogPosts = await _blogRepository.GetLastBlogPostsListAsync();
        if (blogPosts == null)
        {
            return null;
        }

        List<BlogPostCardViewModel> model = blogPosts.Select(p => new BlogPostCardViewModel
        {
            PostId = p.ID,
            Title = p.Title,
            ShortDiscription = p.ShortDiscription,
            ImageName = p.ImageName,
            CreateDate = p.CreateDate.ToShamsi()
        }).ToList();
        return model;

    }

    public async Task<BlogPostDetailsViewModel?> GetSingleBlogPostAsync(int postId)
    {
        var post = await _blogRepository.GetSingleBlogPostAsync(postId);
        if (post == null)
        {
            return null;
        }

        BlogPostDetailsViewModel model = new BlogPostDetailsViewModel
        {
            PostId = post.ID,
            Title = post.Title,
            Discription = post.Discription,
            PostGroup = post.BlogPostGroup.BlogGroup,
            ImageName = post.ImageName,
            PostViews = post.Views.Count,
            PostTags = post.BlogPostTags.Where(t=>t.PostId == postId).Select(t=>t.BlogTag)
                .Select(t=>new BlogTag
                {
                    ID = t.ID,
                    CreateDate = t.CreateDate,
                    isDelete = t.isDelete,
                    TagName = t.TagName,
                }).ToList(),
            PostComments = null,
            CreateDate = post.CreateDate.ToShamsi()
        };

        return model;
    }

    public async Task AddBlogPostViewAsync(int postId)
    {
        BlogPostView model = new BlogPostView
        {
            CreateDate = DateTime.Now,
            isDelete = false,
            BlogPostId = postId,
        };
        await _blogRepository.AddBlogPostViewAsync(model);
        await _blogRepository.SaveAsync();
    }

    public async Task<List<BlogPostMostViewedViewModel>?> GetBlogMostViewedPost()
    {
        var list = await _blogRepository.GetBlogMostViewedPostAsync();
        if (list == null)
        {
            return null;
        }

        List<BlogPostMostViewedViewModel> model = list.Select(p => new BlogPostMostViewedViewModel
        {
            PostId = p.ID,
            Title = p.Title,
            ImageName = p.ImageName,
            PostGroup = p.BlogPostGroup.BlogGroup.GroupName,
            CreateDate = p.CreateDate.ToShamsi()
        }).ToList();

        return model;
    }
}