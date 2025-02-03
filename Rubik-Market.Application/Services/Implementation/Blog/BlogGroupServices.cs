using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Blog.BlogGroups;

namespace Rubik_Market.Application.Services.Implementation.Blog;

public class BlogGroupServices : IBlogGroupServices
{
    #region Constructor

    private readonly IBlogRepository _blogRepository;

    public BlogGroupServices(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    #endregion


    public async Task<List<BlogGroupViewModel>?> GetBlogGroupsAsync()
    {
        var list = await _blogRepository.GetBlogGroupListAsync();

        var model = list?.Select(b => new BlogGroupViewModel
        {
            GroupId = b.ID,
            GroupName = b.GroupName
        }).ToList();

        return model;
    }

    public async Task<AddBlogGroupResult> AddBlogGroupAsync(AddBlogGroupViewModel model)
    {
        try
        {
            BlogGroup blogGroup = new BlogGroup()
            {

                CreateDate = DateTime.Now,
                isDelete = false,
                GroupName = model.GroupName
            };
            await _blogRepository.CreateBlogGroupAsync(blogGroup);
            await _blogRepository.SaveAsync();
            return AddBlogGroupResult.Success;
        }
        catch
        {
            return AddBlogGroupResult.Error;
        }

    }

    public async Task<EditBlogGroupViewModel?> GetBlogGroupByIdAsync(int id)
    {
        var item = await _blogRepository.GetBlogGroupByIdAsync(id);
        if (item == null)
        {
            return null;
        }

        EditBlogGroupViewModel model = new EditBlogGroupViewModel()
        {
            GroupId = item.ID,
            GroupName = item.GroupName,
            IsDelete = item.isDelete
        };

        return model;
    }

    public async Task<EditBlogGroupResult> EditBlogGroupAsync(EditBlogGroupViewModel model)
    {
        var item = await _blogRepository.GetBlogGroupByIdAsync(model.GroupId);
        if (item == null)
        {
            return EditBlogGroupResult.GroupNotFound;
        }
        try
        {

            item.GroupName = model.GroupName;
            _blogRepository.EditBlogGroupAsync(item);
            await _blogRepository.SaveAsync();
            return EditBlogGroupResult.Success;
        }
        catch
        {
            return EditBlogGroupResult.Error;
        }
    }
}