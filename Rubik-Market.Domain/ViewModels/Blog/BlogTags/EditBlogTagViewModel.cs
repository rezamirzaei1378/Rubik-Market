using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.Blog.BlogTags;

public class EditBlogTagViewModel
{
    public int TagId { get; set; }
    [Display(Name = "نام کلمه کلیدی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TagName { get; set; }
}

public enum EditBlogTagResult
{
    Success,
    TagNotFound,
    Error
}