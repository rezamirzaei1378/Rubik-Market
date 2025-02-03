using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.Models.Blog;

public class BlogTag : BaseEntity
{
    [Required]
    public string TagName { get; set; }

    #region Relation

    public ICollection<BlogPostTags>? PostTags { get; set; }

    #endregion
}