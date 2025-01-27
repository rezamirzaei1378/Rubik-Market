using Rubik_Market.Domain.ViewModels.FAQ;

namespace Rubik_Market.Application.Services.Contracts;

public interface IFaqServices
{
    Task<List<FaqClientSideViewModel?>> GetFaqClientSideAsync();
    Task<List<FaqAdminSideViewModel?>> GetFaqAdminSideAsync();
    Task<CreateFaqResult> CreateFaqAsync(CreateFaqViewModel model);
    Task<UpdateFaqViewModel> GetFaqForEdit(int id); 
    Task<UpdateFaqResult> UpdateFaqAsync(UpdateFaqViewModel model);
    Task<DeleteFaqResult> DeleteFaqAsync(int id);
    Task<List<FaqAdminSideViewModel?>> GetDeletedFaqAdminSideAsync();
}