namespace Rubik_Market.Domain.ViewModels.UserPanel;

public class UserPersonalInfoViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool isActive { get; set; }
    public bool isAdmin { get; set; }
    public DateTime CreateDate { get; set; }
}