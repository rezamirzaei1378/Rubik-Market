namespace Rubik_Market.Domain.Models;

public class AboutTeam:BaseEntity
{
    public string Name { get; set; }
    public string JobTitle { get; set; }
    public string ShortDescription { get; set; }
    public string TeamMemberImgName { get; set; }
    public string? TwitterUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? RedditUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? ZLinkUrl { get; set; }
}