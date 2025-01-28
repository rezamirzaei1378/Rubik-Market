using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.Address;

public class AddAddressViewModel
{
    public int UserId { get; set; }

    //[Display(Name = "استان")]
    [Required(ErrorMessage = "لطفا استان را وارد کنید")]
    public string Provnice { get; set; }

    //[Display(Name = "شهر")]
    [Required(ErrorMessage = "لطفا شهر را وارد کنید")]
    public string City { get; set; }

    //[Display(Name = "محل")]
    public string? Area { get; set; }

    //[Display(Name = "آدرس")]
    [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
    public string UserAddress { get; set; }

    //[Display(Name = "کد پستی")]
    [MaxLength(10,ErrorMessage = "تعداد کارکتر وارد شده مجاز نیست")]
    [MinLength(10,ErrorMessage = "تعداد کارکتر وارد شده مجاز نیست")]
    public string? PostalCode { get; set; }

    //[Display(Name = "نام گیرنده")]
    public string? ConsigneeName { get; set; }

    //[Display(Name = "شماره تماس گیرنده ")]
    public string? ConsigneePhoneNumber { get; set; }

}

public enum AddAddressResult
{
    Success,
    Error,
    UserNotFound
}