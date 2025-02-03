using System.ComponentModel.DataAnnotations.Schema;

namespace Rubik_Market.Domain.Models.Blog;

public class BlogPostTags : BaseEntity
{
    public int PostId { get; set; }

    [ForeignKey("PostId")]
    public BlogPosts Posts { get; set; }

    public int TagId { get; set; }

    [ForeignKey("TagId")]
    public BlogTag BlogTag { get; set; }
}