using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.FAQ;

public class FaqClientSideViewModel
{
    public string? Question { get; set; }
    public string? Answer { get; set; }

}

public class FaqAdminSideViewModel
{
    public int? Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public string CreateDate { get; set; }
    public bool? IsDelete { get; set; }

}

public class CreateFaqViewModel
{
    [Display(Name = "سوال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Question { get; set; }

    [Display(Name = "پاسخ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Answer { get; set; }
}

public enum CreateFaqResult
{
    Success,
    Error
}

public class UpdateFaqViewModel
{
    public int Id { get; set; }

    [Display(Name = "سوال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Question { get; set; }

    [Display(Name = "پاسخ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Answer { get; set; }
}

public enum UpdateFaqResult
{
    Success,
    Error
}


public enum DeleteFaqResult
{
    Success,
    Error
}

