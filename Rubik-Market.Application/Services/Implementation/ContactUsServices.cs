using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.ContactUs;

namespace Rubik_Market.Application.Services.Implementation;

public class ContactUsServices : IContactUsServices
{
    #region Constructor

    private readonly IContactUsRepository _contactUsRepository;
    private readonly IEmailSender _emailSender;

    public ContactUsServices(IContactUsRepository contactUsRepository, IEmailSender emailSender)
    {
        _contactUsRepository = contactUsRepository;
        _emailSender = emailSender;
    }

    #endregion


    #region Methods


    public async Task<CreateContactUsResult> CreateContactUsAsync(CreateContactUsViewModel model)
    {
        try
        {
            ContactUs contactUs = new ContactUs
            {
                CreateDate = DateTime.Now,
                isDelete = false,
                FullName = model.FullName,
                Mobile = model.Mobile,
                Email = model.Email,
                Title = model.Title,
                Description = model.Description,
                Answer = null
            };
            await _contactUsRepository.CreateContactUsAsync(contactUs);
            await _contactUsRepository.SaveAsync();
            return CreateContactUsResult.Success;
        }
        catch
        {
            return CreateContactUsResult.Error;
        }

    }

    public async Task<List<ContactUsListViewModel>?> GetContactUsListAsync()
    {
        var contactUsList = await _contactUsRepository.GetContactUsListAsync();
        if (contactUsList == null)
        {
            return null;
        }

        List<ContactUsListViewModel> model = contactUsList.Select(c => new ContactUsListViewModel
        {
            Id = c.ID,
            FullName = c.FullName,
            Email = c.Email,
            Title = c.Title,
            Answer = c.Answer,
            CreateDate = c.CreateDate.ToShamsi()
        }).ToList();
        return model;
    }

    public async Task<ContactUsAnswerViewModel?> GetContactUsBIdAsync(int id)
    {
        var contactUs = await _contactUsRepository.GetContactUsByIdAsync(id);
        if (contactUs == null)
        {
            return null;
        }

        ContactUsAnswerViewModel model = new ContactUsAnswerViewModel
        {
            Id = contactUs.ID,
            FullName = contactUs.FullName,
            Mobile = contactUs.Mobile,
            Email = contactUs.Email,
            Title = contactUs.Title,
            Description = contactUs.Description,
            Answer = contactUs.Answer
        };
        return model;
    }

    public async Task<AnswerResult> ContactUsAnswerResultAsync(ContactUsAnswerViewModel model)
    {
        var contactUs = await _contactUsRepository.GetContactUsByIdAsync(model.Id);
        if (contactUs == null)
        {
            return AnswerResult.ContactUsNotFound;
        }

        if (string.IsNullOrEmpty(model.Answer) || string.IsNullOrEmpty(model.Answer))
        {
            return AnswerResult.AnswerIsNull;
        }

        try
        {
            contactUs.Answer = model.Answer;
            _contactUsRepository.UpdateContactUs(contactUs);
            await _contactUsRepository.SaveAsync();
            string emailBody = $"<h2> پاسخ تماس با ما شما با عنوان{model.Title} به شرح زیر است</h2> "
                + $"<h2>{model.Answer}</h2>";
            _emailSender.SendEmail(model.Email, "پاسخ تماس با ما",emailBody);

            return AnswerResult.Success;
        }
        catch
        {
            return AnswerResult.Error;
        }
    } 

    #endregion
}