using Rubik_Market.Domain.Models;

namespace Rubik_Market.Domain.Repo.Contracts;

public interface IFaqRepository
{
    Task<List<Faq>> GetAllFaqAsync();
    Task CreateFaqAsync(Faq faq);
    Task<Faq?> GetFaqForEditAsync(int id);
    public void UpdateFaq(Faq faq);
    Task SaveAsync();
}