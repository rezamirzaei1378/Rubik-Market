using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.FAQ;

namespace Rubik_Market.Application.Services.Implementation;

public class FaqServices : IFaqServices
{
    #region Constructor

    private readonly IFaqRepository _faqRepository;

    public FaqServices(IFaqRepository faqRepository)
    {
        _faqRepository = faqRepository;
    }

    #endregion


    #region Methods

    public async Task<List<FaqClientSideViewModel?>> GetFaqClientSideAsync()
    {
        var faqList = await _faqRepository.GetAllFaqAsync();
        if (faqList.Count != 0)
        {
            var model = faqList.Where(l => l.isDelete == false);

            return model.Select(l => new FaqClientSideViewModel()
            {
                Answer = l.Answer,
                Question = l.Question
            }).ToList();
        }
        else
        {
            return null;
        }
    }

    public async Task<List<FaqAdminSideViewModel?>> GetFaqAdminSideAsync()
    {
        var faqList = await _faqRepository.GetAllFaqAsync();
        if (faqList != null)
        {
            var model = faqList.Where(l => l.isDelete == false);

            return model.Select(l => new FaqAdminSideViewModel()
            {
                Id = l.ID,
                Answer = l.Answer,
                Question = l.Question,
                CreateDate = l.CreateDate.ToShamsi(),
                IsDelete = l.isDelete
            }).ToList();
        }
        else
        {
            return null;
        }
    }

    public async Task<CreateFaqResult> CreateFaqAsync(CreateFaqViewModel model)
    {
        try
        {
            Faq faq = new Faq()
            {
                Answer = model.Answer,
                Question = model.Question,
                CreateDate = DateTime.Now,
                isDelete = false
            };
            await _faqRepository.CreateFaqAsync(faq);
            await _faqRepository.SaveAsync();
            return CreateFaqResult.Success;
        }
        catch
        {
            return CreateFaqResult.Error;
        }
    }

    public async Task<UpdateFaqViewModel> GetFaqForEdit(int id)
    {
        var faq = await _faqRepository.GetFaqForEditAsync(id);
        if (faq!=null)
        {
            return new UpdateFaqViewModel()
            {
                Id = faq.ID,
                Answer = faq.Answer,
                Question = faq.Question
            };
        }
        else
        {
            return null;
        }
    }

    public async Task<UpdateFaqResult> UpdateFaqAsync(UpdateFaqViewModel model)
    {
        var faq = await _faqRepository.GetFaqForEditAsync(model.Id);
        if (faq == null)
        {
            return UpdateFaqResult.Error;
        }
        try
        {

            faq.ID = model.Id;
            faq.Answer = model.Answer;
            faq.Question = model.Question;

            _faqRepository.UpdateFaq(faq);
            await _faqRepository.SaveAsync();
            return UpdateFaqResult.Success;
        }
        catch
        {
            return UpdateFaqResult.Error;
        }
    }

    public async Task<DeleteFaqResult> DeleteFaqAsync(int id)
    {
        var faq = await _faqRepository.GetFaqForEditAsync(id);
        if (faq == null)
        {
            return DeleteFaqResult.Error;
        }
        try
        {

            faq.isDelete = true;

            _faqRepository.UpdateFaq(faq);
            await _faqRepository.SaveAsync();
            return DeleteFaqResult.Success;
        }
        catch
        {
            return DeleteFaqResult.Error;
        }
    }

    public async Task<List<FaqAdminSideViewModel?>> GetDeletedFaqAdminSideAsync()
    {
        var faqList = await _faqRepository.GetAllFaqAsync();
        if (faqList != null)
        {
            var model = faqList.Where(l => l.isDelete == true);

            return model.Select(l => new FaqAdminSideViewModel()
            {
                Id = l.ID,
                Answer = l.Answer,
                Question = l.Question,
                CreateDate = l.CreateDate.ToShamsi(),
                IsDelete = l.isDelete
            }).ToList();
        }
        else
        {
            return null;
        }
    }

    #endregion
}