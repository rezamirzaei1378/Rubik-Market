﻿using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Domain.ViewModels.Blog.BlogGroups;
using Rubik_Market.Domain.ViewModels.Blog.BlogPost;
using Rubik_Market.Domain.ViewModels.Blog.BlogTags;

namespace Rubik_Market.Web.Areas.Admin.Controllers
{
    public class BlogController : AdminBaseController
    {
        #region Constructor

        private readonly IBlogGroupServices _blogGroupServices;
        private readonly IBlogTagServices _blogTagServices;
        private readonly IBlogPostServices _blogPostServices;

        public BlogController(IBlogGroupServices blogGroupServices,IBlogTagServices blogTagServices, IBlogPostServices blogPostServices)
        {
            _blogGroupServices = blogGroupServices;
            _blogTagServices = blogTagServices;
            _blogPostServices = blogPostServices;
        }

        #endregion

        #region Groups

        #region GroupList

        [HttpGet("BlogGroups")]
        public async Task<IActionResult> BlogGroupList()
        {
            var model = await _blogGroupServices.GetBlogGroupsAsync();
            return View(model);
        }

        #endregion

        #region AddGroup

        [HttpGet("Create-BlogGroups")]
        public IActionResult AddBlogGroup()
        {
            return View();
        }

        [HttpPost("Create-BlogGroups")]
        public async Task<IActionResult> AddBlogGroup(AddBlogGroupViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _blogGroupServices.AddBlogGroupAsync(model);
            switch (result)
            {
                case AddBlogGroupResult.Success:
                    TempData[SuccessMessage] = "گروه با موفقبت اضافه شد";
                    return RedirectToAction(nameof(BlogGroupList));
                case AddBlogGroupResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }
            return View(model);
        }

        #endregion

        #region EditGroup

        [HttpGet("Edit-BlogGroups")]
        public async Task<IActionResult> EditBlogGroup(int id)
        {
            var item = await _blogGroupServices.GetBlogGroupByIdAsync(id);
            if (item == null)
            {
                TempData[ErrorMessage] = "گروه یافت نشد";
                return RedirectToAction(nameof(BlogGroupList));
            }
            return View(item);
        }

        [HttpPost("Edit-BlogGroups")]
        public async Task<IActionResult> EditBlogGroup(EditBlogGroupViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _blogGroupServices.EditBlogGroupAsync(model);

            switch (result)
            {
                case EditBlogGroupResult.Success:
                    TempData[SuccessMessage] = "گروه با موفقبت ویرایش شد";
                    return RedirectToAction(nameof(BlogGroupList));
                case EditBlogGroupResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case EditBlogGroupResult.GroupNotFound:
                    TempData[ErrorMessage] = "گروه یافت نشد";
                    break;
            }

            return View(model);

        }

        #endregion

        #endregion

        #region Tags

        #region List

        [HttpGet("BlogTags")]
        public async Task<IActionResult> BlogTagList()
        {
            var model = await _blogTagServices.GetBlogTagsAsync();
            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("Add-BlogTags")]
        public IActionResult AddBlogTag()
        {
            return View();
        }

        [HttpPost("Add-BlogTags")]
        public async Task<IActionResult> AddBlogTag(AddBlogTagViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _blogTagServices.AddBlogTagAsync(model);

            switch (result)
            {
                case AddBlogTagResult.Success:
                    TempData[SuccessMessage] = "کلمه کلیدی با موفقبت اضافه شد";
                    return RedirectToAction(nameof(BlogTagList));
                case AddBlogTagResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }

            return View(model);
        }



        #endregion

        #region Edit

        [HttpGet("Edit-BlogTags")]
        public async Task<IActionResult> EditBlogTag(int tagId)
        {
            var item = await _blogTagServices.GetBlogTagByIdAsync(tagId);
            if (item == null)
            {
                TempData[ErrorMessage] = "کلمه کلیدی یافت نشد";
                return RedirectToAction(nameof(BlogTagList));
            }
            return View(item);
        }

        [HttpPost("Edit-BlogTags")]
        public async Task<IActionResult> EditBlogTag(EditBlogTagViewModel model)
        {

            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion
            var result = await _blogTagServices.EditBlogTagAsync(model);

            switch (result)
            {
                case EditBlogTagResult.Success:
                    TempData[SuccessMessage] = "کلمه کلیدی با موفقبت ویرایش شد";
                    return RedirectToAction(nameof(BlogTagList));
                case EditBlogTagResult.TagNotFound:
                    TempData[ErrorMessage] = "کلمه کلیدی یافت نشد";
                    break;
                case EditBlogTagResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }

            return View(model);
        }

        #endregion

        #endregion

        #region Posts

        #region List

        public async Task<IActionResult> PostsList()
        {
            var model = await _blogPostServices.GetPostsAsync();
            return View(model);
        }

        #endregion

        #region Create

        [HttpGet("Create-BlogPost")]
        public async Task<IActionResult> CreateBlogPost()
        {
            var model = await _blogPostServices.GetBlogPostTagsAndGroupsAsync();
            return View(model);
        }

        [HttpPost("Create-BlogPost")]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _blogPostServices.CreateBlogPost(model);
            switch (result)
            {
                case CreateBlogPostResult.Success:
                    TempData[SuccessMessage] = "پست با موفقبت اضافه شد";
                    return RedirectToAction(nameof(PostsList));
                case CreateBlogPostResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }

            return View(model);
        }

        #endregion

        #region Edit

        [HttpGet("Edit-BlogPost")]
        public async Task<IActionResult> EditBlogPost(int postId)
        {
            var post = await _blogPostServices.GetBlogForEditAsync(postId);
            if (post == null)
            {
                TempData[ErrorMessage] = "پستی یافت نشد";
                return RedirectToAction(nameof(BlogTagList));
            }
            return View(post);
        }

        [HttpPost("Edit-BlogPost")]
        public async Task<IActionResult> EditBlogPost(EditBlogPostViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _blogPostServices.EditBlogPostAsync(model);
            switch (result)
            {
                case EditBlogPostResult.Success:
                    TempData[SuccessMessage] = "پست با موفقبت ویرایش شد";
                    return RedirectToAction(nameof(PostsList));
                case EditBlogPostResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
                case EditBlogPostResult.PostNotFound:
                    TempData[ErrorMessage] = "پست پیدا نشد";
                    return RedirectToAction(nameof(PostsList));
            }

            return View(model);
        }

        #endregion

        #region Delete

        [HttpGet("Delete-BlogPost")]
        public async Task<IActionResult> DeleteBlogPost(int postId)
        {
            var result = await _blogPostServices.DeleteBlogPostAsync(postId);

            switch (result)
            {
                case DeleteBLogPostResult.Success:
                    TempData[SuccessMessage] = "پست با موفقبت حدف شد";
                    break;
                case DeleteBLogPostResult.PostNotFound:
                    TempData[ErrorMessage] = "پست پیدا نشد";
                    break;
                case DeleteBLogPostResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است";
                    break;
            }

            return RedirectToAction(nameof(PostsList));
        }

        #endregion

        #endregion
    }
}
