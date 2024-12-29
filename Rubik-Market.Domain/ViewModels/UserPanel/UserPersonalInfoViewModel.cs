namespace Rubik_Market.Domain.ViewModels.UserPanel;

public class UserPersonalInfoViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool isActive { get; set; }
    public bool isAdmin { get; set; }
    public string? NationalCode { get; set; }
    public string? CellPhoneNumber { get; set; }
    public string? HousePhoneNumber { get; set; }
    public string? BirthDate { get; set; }
    public string? CardNumberForRejectMoney { get; set; }
    public DateTime UserCreateDate { get; set; }
}