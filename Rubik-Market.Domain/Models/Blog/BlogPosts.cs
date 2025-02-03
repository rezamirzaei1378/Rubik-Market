using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.Models.Blog;

public class BlogPosts : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string ShortDiscription { get; set; }
    public string Discription { get; set; }
    public string ImageName { get; set; }


    #region Relation

    public ICollection<BlogPostTags>? Tags { get; set; }
    public ICollection<BlogPostComment>? Comments { get; set; }
    public ICollection<BlogPostView>? Views { get; set; }

    #endregion

}