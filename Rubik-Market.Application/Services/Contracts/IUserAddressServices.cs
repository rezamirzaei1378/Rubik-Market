using Rubik_Market.Domain.ViewModels.Address;

namespace Rubik_Market.Application.Services.Contracts;

public interface IUserAddressServices
{
    Task<List<ClientSideAddressViewModel?>> GetUserAddressesAsync(int userId);
    Task<AdminSideAddressViewModel> GetAllUserAddressesAsync();
    Task<AddAddressResult> AddAddressAsync(AddAddressViewModel model);
    Task<EditAddressViewModel?> GetAddressForEditAsync(int addressId);
    Task<EditAddressResult> EditAddressAsync(EditAddressViewModel model);
    Task<DeleteAddressResult> DeleteAddressAsync(int addressId);
    Task<ChangeCurrentAddressResult> ChangeCurrentAddressAsync(int addressId);
}