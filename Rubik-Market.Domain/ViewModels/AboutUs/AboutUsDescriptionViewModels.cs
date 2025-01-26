namespace Rubik_Market.Domain.ViewModels.AboutUs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class AboutUsDescriptionViewModel
{
    public int? ID { get; set; }
    public string? AboutUsText { get; set; }
    public string? AboutUsImageName { get; set; }
}

public class AddAboutUsDescriptionViewModel
{
    public string AboutUsText { get; set; }
    public IFormFile? Image { get; set; }
}

public enum AddAboutUsDescriptionResult
{
    Success,
    Error
}

public class EditAboutUsDescriptionViewModel
{
    public int Id { get; set; }
    public string? AboutUsText { get; set; }
    public string? AboutUsImageName { get; set; }
    public IFormFile? Image { get; set; }
}

public enum EditAboutUsDescriptionResult
{
    Success,
    Error
}


public class TeamMembersViewModel
{
    public int? ID { get; set; }
    public string? TemaMemberName { get; set; }
    public string? TemaMemberJobTitle { get; set; }
    public string? TemaMemberShortDescription { get; set; }
    public string? TeamMemberImgName { get; set; }
    public string? TwitterUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? RedditUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? ZLinkUrl { get; set; }
    public DateTime CreateDate { get; set; }
    public bool? isDelete { get; set; }
}

public class AddTeamMemberViewModel
{
    [Display(Name = "اسم عضو جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TemaMemberName { get; set; }

    [Display(Name = "عنوان عضو جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TemaMemberJobTitle { get; set; }

    [Display(Name = "توضیح مختصر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TemaMemberShortDescription { get; set; }

    [Display(Name = "عکس")]
    public string TeamMemberImgName { get; set; }

    [Display(Name = "Twitter")]
    public string? TwitterUrl { get; set; }

    [Display(Name = "Linked")]
    public string? LinkedInUrl { get; set; }

    [Display(Name = "Reddit")]
    public string? RedditUrl { get; set; }

    [Display(Name = "Github")]
    public string? GithubUrl { get; set; }

    [Display(Name = "ZLink")]
    public string? ZLinkUrl { get; set; }
    public IFormFile? Image { get; set; }
}

public enum AddTeamMemberResult
{
    Success,
    Error
}

public class EditTeamMemberViewModel
{
    public int Id { get; set; }
    [Display(Name = "اسم عضو تیم")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TemaMemberName { get; set; }
    [Display(Name = "عنوان عضو تیم")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TemaMemberJobTitle { get; set; }

    [Display(Name = "توضحیات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TemaMemberShortDescription { get; set; }

    public string TeamMemberImgName { get; set; }

    [Display(Name = "Twitter")]
    public string? TwitterUrl { get; set; }

    [Display(Name = "LinkedIn")]
    public string? LinkedInUrl { get; set; }

    [Display(Name = "Reddit")]
    public string? RedditUrl { get; set; }

    [Display(Name = "Github")]
    public string? GithubUrl { get; set; }

    [Display(Name = "ZLink")]
    public string? ZLinkUrl { get; set; }
    public DateTime CreateDate { get; set; }
    public bool? isDelete { get; set; }

    [Display(Name = "عکس")]
    public IFormFile? Image { get; set; }
}

public enum EditTeamMemberResult
{
    Success,
    Error
}


public enum DeleteTeamMemberResult
{
    Success,
    Error
}