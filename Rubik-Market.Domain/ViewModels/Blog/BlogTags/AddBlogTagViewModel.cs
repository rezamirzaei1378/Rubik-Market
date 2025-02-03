using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.Blog.BlogTags;

public class AddBlogTagViewModel
{
    [Display(Name = "نام کلمه کلیدی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TagName { get; set; }
}

public enum AddBlogTagResult
{
    Success,
    Error
}