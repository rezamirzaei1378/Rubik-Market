using System.ComponentModel.DataAnnotations.Schema;

namespace Rubik_Market.Domain.Models;

public class UserProfileInfo:BaseEntity
{
    public int UserId { get; set; }
    public string? NationalCode { get; set; }
    public string? CellPhoneNumber { get; set; }
    public string? HousePhoneNumber { get; set; }
    public string? BirthDate { get; set; }
    public string? CardNumberForRejectMoney { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}