using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Blog.BlogTags;

namespace Rubik_Market.Application.Services.Implementation.Blog;

public class BlogTagServices : IBlogTagServices
{
    #region Constructor

    private readonly IBlogRepository _blogRepository;

    public BlogTagServices(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    #endregion

    public async Task<List<BlogTagsViewModel>?> GetBlogTagsAsync()
    {
        var tagList = await _blogRepository.GetBlogTagListAsync();
        if (tagList == null)
        {
            return null;
        }

        var model = tagList.Select(t => new BlogTagsViewModel
        {
            TagId = t.ID,
            TagName = t.TagName
        }).ToList();

        return model;
    }

    public async Task<AddBlogTagResult> AddBlogTagAsync(AddBlogTagViewModel blogTag)
    {
        try
        {
            BlogTag tag = new BlogTag
            {
                CreateDate = DateTime.Now,
                isDelete = false,
                TagName = blogTag.TagName,
            };

            await _blogRepository.CreateBlogTagAsync(tag);
            await _blogRepository.SaveAsync();
            return AddBlogTagResult.Success;

        }
        catch
        {
            return AddBlogTagResult.Error;
        }
    }

    public async Task<EditBlogTagViewModel?> GetBlogTagByIdAsync(int blogTagId)
    {
        var tag = await _blogRepository.GetBlogTagByIdAsync(blogTagId);
        if (tag == null)
        {
            return null;
        }

        var model = new EditBlogTagViewModel
        {
            TagId = tag.ID,
            TagName = tag.TagName
        };
        return model;
    }

    public async Task<EditBlogTagResult> EditBlogTagAsync(EditBlogTagViewModel blogTag)
    {
        var tag = await _blogRepository.GetBlogTagByIdAsync(blogTag.TagId);
        if (tag == null)
        {
            return EditBlogTagResult.TagNotFound;
        }

        try
        {
            tag.TagName = blogTag.TagName;
            _blogRepository.EditBlogTagAsync(tag);
            await _blogRepository.SaveAsync();
            return EditBlogTagResult.Success;
        }
        catch
        {
            return EditBlogTagResult.Error;
        }
    }
}