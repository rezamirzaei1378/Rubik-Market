using Rubik_Market.Domain.Models.Blog;

namespace Rubik_Market.Domain.ViewModels.Blog.BlogArea;

public class BlogPostDetailsViewModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Discription { get; set; }
    public BlogGroup PostGroup { get; set; }
    public string ImageName { get; set; }
    public int PostViews { get; set; }
    public List<BlogTag> PostTags{ get; set; }
    public List<BlogPostComment>? PostComments { get; set; }
    public string CreateDate { get; set; }
}