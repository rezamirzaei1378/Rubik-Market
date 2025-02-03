using System.ComponentModel.DataAnnotations;

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
    public string ImageName { get; set; }

    [Display(Name = "عکس اصلی مقاله")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Image { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
    public string PostGroup { get; set; }

    [Display(Name = "کلمات کلیدی")]
    [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
    public List<string>? PostTags { get; set; }

}

public enum EditBlogPostResult
{
    Success,
    PostNotFound,
    Error
}