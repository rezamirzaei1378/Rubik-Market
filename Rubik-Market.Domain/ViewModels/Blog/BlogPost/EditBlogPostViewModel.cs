using Rubik_Market.Domain.Models.Blog;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Rubik_Market.Domain.ViewModels.Blog.BlogPost;

public class EditBlogPostViewModel
{
    public int PostId { get; set; }
    [Display(Name = "عنوان مقاله")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(150, ErrorMessage = "تعداد کارکتر بیش از حد مجاز است")]
    public string Title { get; set; }

    [Display(Name = "توضیح مختصر مقاله")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ShortDiscription { get; set; }

    [Display(Name = "توضیح مقاله")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Discription { get; set; }
    public string? ImageName { get; set; }

    [Display(Name = "عکس اصلی مقاله")]
    public IFormFile? Image { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
    public int PostGroupId { get; set; }

    [Display(Name = "دسته بندی")]
    public List<BlogGroup>? BlogGroups { get; set; } = new List<BlogGroup>();

    [Display(Name = "کلمات کلیدی")]
    [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
    public List<int> PostTags { get; set; }

    [Display(Name = "کلمات کلیدی")]
    public List<BlogTag>? BlogTags { get; set; } = new List<BlogTag>();

}

public enum EditBlogPostResult
{
    Success,
    PostNotFound,
    Error
}

public enum DeleteBLogPostResult
{
    Success,
    PostNotFound,
    Error
}