namespace Rubik_Market.Domain.Models;

public class ContactUs:BaseEntity
{
    public string FullName { get; set; }
    public string? Mobile { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Answer { get; set; }
}