namespace Rubik_Market.Domain.ViewModels.Blog.BlogPost;

public class BlogPostViewModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string ShortDiscription { get; set; }
    public string PostGroup { get; set; }
    public string? PostViews { get; set; }
    public string CreateDate { get; set; }
}