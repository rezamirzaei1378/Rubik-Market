namespace Rubik_Market.Domain.ViewModels.Blog.BlogTags;

public class AddBlogTagViewModel
{
    public string TagName { get; set; }
}

public enum AddBlogTagResult
{
    Success,
    Error
}