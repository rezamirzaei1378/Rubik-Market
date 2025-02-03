using System.ComponentModel.DataAnnotations.Schema;

namespace Rubik_Market.Domain.Models.Blog;

public class BlogPostGroup : BaseEntity
{
    public int PostId { get; set; }

    [ForeignKey("PostId")]
    public BlogPosts Posts { get; set; }

    public int GroupId { get; set; }

    [ForeignKey("GroupId")]
    public BlogGroup BlogGroup { get; set; }
}