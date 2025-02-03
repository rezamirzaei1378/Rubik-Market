using System.ComponentModel.DataAnnotations.Schema;

namespace Rubik_Market.Domain.Models.Blog;

public class BlogPostView : BaseEntity
{
    public int BlogPostId { get; set; }

    [ForeignKey("BlogPostId")]
    public BlogPosts BlogPosts { get; set; }
}