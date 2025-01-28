using System.ComponentModel.DataAnnotations.Schema;

namespace Rubik_Market.Domain.Models;

public class Address:BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }


    public string Provnice { get; set; }
    public string City { get; set; }
    public string? Area { get; set; }
    public string UserAddress { get; set; }
    public string? PostalCode { get; set; }
    public string? ConsigneeName { get; set; }
    public string? ConsigneePhoneNumber { get; set; }
    public bool CurrentAddress { get; set; }
}