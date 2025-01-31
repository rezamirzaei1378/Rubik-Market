using Rubik_Market.Domain.ViewModels.ContactUs;

namespace Rubik_Market.Application.Services.Contracts;

public interface IContactUsServices
{
    Task<CreateContactUsResult> CreateContactUsAsync(CreateContactUsViewModel model);
    Task<List<ContactUsListViewModel>?> GetContactUsListAsync();
    Task<ContactUsAnswerViewModel?> GetContactUsBIdAsync(int id);
    Task<AnswerResult> ContactUsAnswerResultAsync(ContactUsAnswerViewModel model);
}