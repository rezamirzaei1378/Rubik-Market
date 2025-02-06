namespace Rubik_Market.Domain.Models.Blog;

public class BlogGroup : BaseEntity
{
    public string GroupName { get; set; }


    public BlogPostGroup BlogPostGroup { get; set; }
}