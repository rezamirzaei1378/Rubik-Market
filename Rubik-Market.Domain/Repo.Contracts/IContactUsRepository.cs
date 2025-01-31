using Rubik_Market.Domain.Models;

namespace Rubik_Market.Domain.Repo.Contracts;

public interface IContactUsRepository
{
    Task<List<ContactUs>?> GetContactUsListAsync();
    Task CreateContactUsAsync(ContactUs contactUs);
    Task<ContactUs?> GetContactUsByIdAsync(int id);
    public void UpdateContactUs(ContactUs contactUs);
    Task SaveAsync();
}