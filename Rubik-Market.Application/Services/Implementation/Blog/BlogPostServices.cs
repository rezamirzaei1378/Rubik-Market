using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Application.Tools;
using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Blog.BlogPost;

namespace Rubik_Market.Application.Services.Implementation.Blog;

public class BlogPostServices : IBlogPostServices
{
    #region Constructor

    private readonly IBlogRepository _blogRepository;

    public BlogPostServices(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    #endregion


    public async Task<List<BlogPostViewModel>?> GetPostsAsync()
    {
        var list = await _blogRepository.GetPostsAsync();
        if (list == null)
        {
            return null;
        }

        var model = list.Select(p => new BlogPostViewModel()
        {
            PostId = p.ID,
            Title = p.Title,
            ShortDiscription = p.ShortDiscription,
            PostGroup = p.BlogPostGroup.BlogGroup.GroupName,
            CreateDate = p.CreateDate.ToShamsi()
        }).ToList();
        return model;
    }

    public async Task<CreateBlogPostResult> CreateBlogPost(CreateBlogPostViewModel model)
    {
        try
        {
            var post = new BlogPosts
            {
                CreateDate = DateTime.Now,
                isDelete = false,
                Title = model.Title,
                ShortDiscription = model.ShortDiscription,
                Discription = model.Discription,
                BlogPostGroup = new BlogPostGroup(){ GroupId = model.PostGroupId},
                BlogPostTags = model.PostTags.Select(t=>new BlogPostTags(){TagId = t}).ToList()
            };

            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.BlogPostImage, 350, 645, SiteTools.BlogPostImageThumbPath);
                post.ImageName = imageName;
            }
            else
            {
                post.ImageName = "Default.png";
            }
            await _blogRepository.AddBlogPost(post);
            await _blogRepository.SaveAsync();
            return CreateBlogPostResult.Success;
        }
        catch
        {
            return CreateBlogPostResult.Error;
        }
    }

    public async Task<CreateBlogPostViewModel> GetBlogPostTagsAndGroupsAsync()
    {
        var groupList = await _blogRepository.GetBlogGroupListAsync();
        var tagList = await _blogRepository.GetBlogTagListAsync();
        var model = new CreateBlogPostViewModel()
        {
            BlogGroups = groupList,
            BlogTags = tagList
        };
        return model;
    }

    public async Task<EditBlogPostViewModel?> GetBlogForEditAsync(int postId)
    {
        var post = await _blogRepository.GetBlogPostByIdAsync(postId);
        if (post == null)
        {
            return null;
        }

        var groupList = await _blogRepository.GetBlogGroupListAsync();
        var tagList = await _blogRepository.GetBlogTagListAsync();
        var selectedTagList = await _blogRepository.GetBlogPostTagsByIdAsync(postId);

        EditBlogPostViewModel model = new EditBlogPostViewModel
        {
            PostId = post.ID,
            Title = post.Title,
            ShortDiscription = post.ShortDiscription,
            Discription = post.Discription,
            ImageName = post.ImageName,
            Image = post.ImageName,
            PostGroupId = post.BlogPostGroup.GroupId,
            BlogGroups = groupList,
            PostTags = selectedTagList,
            BlogTags = tagList
        };
        return model;

    }
}