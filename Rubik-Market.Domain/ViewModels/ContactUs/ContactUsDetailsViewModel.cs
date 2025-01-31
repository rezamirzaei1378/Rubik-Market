using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.ContactUs;

public class ContactUsListViewModel
{
    public int? Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Title { get; set; }
    public string? Answer { get; set; }
    public string? CreateDate { get; set; }
}
public class ContactUsAnswerViewModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    [Display(Name = "پاسخ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string? Answer { get; set; }
}


public enum AnswerResult
{
    Success,
    ContactUsNotFound,
    Error,
    AnswerIsNull
}