using System.ComponentModel.DataAnnotations.Schema;

namespace Rubik_Market.Domain.Models.Blog;

public class BlogPostComment : BaseEntity
{
    public string Comment { get; set; }
    public int UserId { get; set; }
    public int BlogPostId { get; set; }
    public string? CommentIdAsAnswer { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("BlogPostId")]
    public BlogPosts BlogPosts { get; set; }
}