using System.ComponentModel.DataAnnotations;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Application.Tools;
using Rubik_Market.Domain.Models.Blog;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Blog.BlogPost;
using BlogPostTags = Rubik_Market.Domain.Models.Blog.BlogPostTags;

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
                BlogPostGroup = new BlogPostGroup { GroupId = model.PostGroupId },
                BlogPostTags = model.PostTags.Select(t => new BlogPostTags() { TagId = t }).ToList()
            };

            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.BlogPostImage, 350, 645,
                    SiteTools.BlogPostImageThumbPath);
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
            PostGroupId = post.BlogPostGroup.GroupId,
            BlogGroups = groupList,
            PostTags = selectedTagList,
            BlogTags = tagList
        };
        return model;

    }

    public static (List<int> TagsToRemove, List<int> TagsToAdd) CompareLists(List<int> CurrentTagsList,
        List<int> NewTagsList)
    {
        HashSet<int> set1 = new HashSet<int>(CurrentTagsList);
        HashSet<int> set2 = new HashSet<int>(NewTagsList);

        List<int> TagsToRemove = CurrentTagsList.Where(num => !set2.Contains(num)).ToList();
        List<int> TagsToAdd = NewTagsList.Where(num => !set1.Contains(num)).ToList();

        return (TagsToRemove, TagsToAdd);
    }

    public async Task<EditBlogPostResult> EditBlogPostAsync(EditBlogPostViewModel model)
    {
        var post = await _blogRepository.GetBlogPostByIdAsync(model.PostId);
        var selectGroup = await _blogRepository.GetBlogGroupByIdAsync(model.PostGroupId);
        var selectedTagList = await _blogRepository.GetBlogPostTagsByIdAsync(model.PostId);
        if (post == null)
        {
            return EditBlogPostResult.PostNotFound;
        }

        try
        {
            var (TagsToRemove, TagsToAdd) = CompareLists(selectedTagList, model.PostTags);

            post.Title = model.Title;
            post.Discription = model.Discription;
            post.ShortDiscription = model.ShortDiscription;
            post.BlogPostGroup.BlogGroup = selectGroup;

            if (model.Image != null)
            {
                string imageName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(imageName, SiteTools.BlogPostImage, 350, 645, SiteTools.BlogPostImageThumbPath, post.ImageName);
                post.ImageName = imageName;
            }

            if (TagsToRemove.Count != 0 && TagsToAdd.Count != 0)
            {
                var tagsList = await _blogRepository.GetTagsListToRemove(post.ID, TagsToRemove);
                _blogRepository.BlogPostTagToRemove(tagsList);
                post.BlogPostTags = await _blogRepository.GetTagsListToNotRemove(post.ID, TagsToRemove);
                post.BlogPostTags = TagsToAdd.Select(t => new BlogPostTags() { TagId = t }).ToList();
            }
            else if (TagsToRemove.Count != 0)
            {
                var tagsList = await _blogRepository.GetTagsListToRemove(post.ID, TagsToRemove);
                _blogRepository.BlogPostTagToRemove(tagsList);
                post.BlogPostTags = await _blogRepository.GetTagsListToNotRemove(post.ID, TagsToRemove);
            }
            else if (TagsToAdd.Count != 0)
            {
                post.BlogPostTags = TagsToAdd.Select(t => new BlogPostTags() { TagId = t }).ToList();
            }

            _blogRepository.UpdateBlogPost(post);
            await _blogRepository.SaveAsync();
            return EditBlogPostResult.Success;
        }
        catch
        {
            return EditBlogPostResult.Error;
        }
    }

    public async Task<DeleteBLogPostResult> DeleteBlogPostAsync(int postId)
    {
        var post = await _blogRepository.GetBlogPostByIdAsync(postId);
        var postGroup = await _blogRepository.GetBlogPostGroupAsync(postId);
        var postTagsList = await _blogRepository.GetBlogPostTagsListAsync(postId);

        if (post == null)
        {
            return DeleteBLogPostResult.PostNotFound;
        }

        try
        {
            post.isDelete = true;
            postGroup.isDelete = true;

            foreach (var item in postTagsList)
            {
                item.isDelete = true;
            }

            _blogRepository.DeletePostTag(post, postTagsList, postGroup);
            await _blogRepository.SaveAsync();
            return DeleteBLogPostResult.Success;
        }
        catch
        {
            return DeleteBLogPostResult.Error;
        }
    }
}