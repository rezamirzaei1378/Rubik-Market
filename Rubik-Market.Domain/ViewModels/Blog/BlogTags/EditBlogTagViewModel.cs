namespace Rubik_Market.Domain.ViewModels.Blog.BlogTags;

public class EditBlogTagViewModel
{
    public int TagId { get; set; }
    public string TagName { get; set; }
}

public enum EditBlogTagResult
{
    Success,
    Error
}