using System.ComponentModel.DataAnnotations;

namespace Rubik_Market.Domain.ViewModels.Address;

public class EditAddressViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [Required(ErrorMessage = "لطفا استان را وارد کنید")]
    public string Provnice { get; set; }
    [Required(ErrorMessage = "لطفا شهر را وارد کنید")]
    public string City { get; set; }
    public string? Area { get; set; }
    [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
    public string UserAddress { get; set; }
    [MaxLength(10, ErrorMessage = "تعداد کارکتر وارد شده مجاز نیست")]
    [MinLength(10, ErrorMessage = "تعداد کارکتر وارد شده مجاز نیست")]
    public string? PostalCode { get; set; }
    public string? ConsigneeName { get; set; }
    public string? ConsigneePhoneNumber { get; set; }
    public bool CurrentAddress { get; set; }
}

public enum EditAddressResult
{
    Success,
    Error,
    UserNotFound,
    AddressNotFound
}

public enum DeleteAddressResult
{
    Success,
    Error,
    AddressNotFound
}

public enum ChangeCurrentAddressResult
{
    Success,
    Error,
    UserNotFound,
    AddressNotFound
}