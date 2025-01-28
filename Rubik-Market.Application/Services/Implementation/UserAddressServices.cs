using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Address;

namespace Rubik_Market.Application.Services.Implementation;

public class UserAddressServices : IUserAddressServices
{
    #region Constructor

    private readonly IUserAddressRepository _userAddressRepository;
    private readonly IUserServices _userServices;

    public UserAddressServices(IUserAddressRepository userAddressRepository, IUserServices userServices)
    {
        _userAddressRepository = userAddressRepository;
        _userServices = userServices;
    }

    #endregion

    #region Methods


    public async Task<List<ClientSideAddressViewModel?>> GetUserAddressesAsync(int userId)
    {
        var userAddresses = await _userAddressRepository.GetUserAddressesAsync(userId);

        if (userAddresses != null)
        {
            List<ClientSideAddressViewModel?> model = userAddresses.Select(a => new ClientSideAddressViewModel
            {
                Id = a.ID,
                UserId = a.UserId,
                Provnice = a.Provnice,
                City = a.City,
                Area = a.Area,
                UserAddress = a.UserAddress,
                PostalCode = a.PostalCode,
                ConsigneeName = a.ConsigneeName,
                ConsigneePhoneNumber = a.ConsigneePhoneNumber,
                CurrentAddress = a.CurrentAddress
            }).ToList();

            return model;
        }
        else
        {
            return null;
        }
    }

    public async Task<AdminSideAddressViewModel> GetAllUserAddressesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AddAddressResult> AddAddressAsync(AddAddressViewModel model)
    {
        var user = await _userServices.GetUserByIdAsync(model.UserId);
        if (user == null)
            return AddAddressResult.UserNotFound;
        var oldAddress = await _userAddressRepository.GetUserCurrentAddressAsync(model.UserId);
        try
        {

            Address address = new Address
            {
                CreateDate = DateTime.Now,
                isDelete = false,
                UserId = model.UserId,
                Provnice = model.Provnice,
                City = model.City,
                Area = model.Area,
                UserAddress = model.UserAddress,
                PostalCode = model.PostalCode,
                ConsigneeName = model.ConsigneeName,
                ConsigneePhoneNumber = model.ConsigneePhoneNumber,
            };
            if (oldAddress != null)
            {
                oldAddress.CurrentAddress = false;
                _userAddressRepository.UpdateUserAddress(oldAddress);
                await _userAddressRepository.SaveAsync();
            }
            else
            {
                address.CurrentAddress = true;
            }

            await _userAddressRepository.AddUserAddressAsync(address);
            await _userAddressRepository.SaveAsync();
            return AddAddressResult.Success;
        }
        catch
        {
            return AddAddressResult.Error;
        }
    }

    public async Task<EditAddressViewModel?> GetAddressForEditAsync(int addressId)
    {
        var address = await _userAddressRepository.GetUserAddressForEditAsync(addressId);

        if (address != null)
        {
            EditAddressViewModel model = new EditAddressViewModel
            {
                Id = address.ID,
                UserId = address.UserId,
                Provnice = address.Provnice,
                City = address.City,
                Area = address.Area,
                UserAddress = address.UserAddress,
                PostalCode = address.PostalCode,
                ConsigneeName = address.ConsigneeName,
                ConsigneePhoneNumber = address.ConsigneePhoneNumber,
                CurrentAddress = address.CurrentAddress
            };
            return model;
        }
        else
        {
            return null;
        }

    }

    public async Task<EditAddressResult> EditAddressAsync(EditAddressViewModel model)
    {
        var user = await _userServices.GetUserByIdAsync(model.UserId);
        var address = await _userAddressRepository.GetUserAddressForEditAsync(model.Id);

        if (user == null)
            return EditAddressResult.UserNotFound;
        if (address == null)
            return EditAddressResult.AddressNotFound;

        try
        {
            if (model.CurrentAddress == true)
            {
                var oldAddress = await _userAddressRepository.GetUserCurrentAddressAsync(address.UserId);
                if (oldAddress != null)
                {
                    oldAddress.CurrentAddress = false;
                    _userAddressRepository.UpdateUserAddress(oldAddress);
                }
            }
            address.UserAddress = model.UserAddress;
            address.ConsigneePhoneNumber = model.ConsigneePhoneNumber;
            address.Area = model.Area;
            address.ConsigneeName = model.ConsigneeName;
            address.City = model.City;
            address.CurrentAddress = model.CurrentAddress;
            address.PostalCode = model.PostalCode;
            address.Provnice = model.Provnice;

            _userAddressRepository.UpdateUserAddress(address);
            await _userAddressRepository.SaveAsync();
            return EditAddressResult.Success;
        }
        catch
        {
            return EditAddressResult.Error;
        }
    }

    public async Task<DeleteAddressResult> DeleteAddressAsync(int addressId)
    {
        var address = await _userAddressRepository.GetUserAddressForEditAsync(addressId);


        if (address == null)
            return DeleteAddressResult.AddressNotFound;

        try
        {
            address.isDelete = true;

            _userAddressRepository.UpdateUserAddress(address);
            await _userAddressRepository.SaveAsync();
            return DeleteAddressResult.Success;
        }
        catch
        {
            return DeleteAddressResult.Error;
        }
    }

    public async Task<ChangeCurrentAddressResult> ChangeCurrentAddressAsync(int addressId)
    {
        var address = await _userAddressRepository.GetUserAddressForEditAsync(addressId);
        var oldAddress = await _userAddressRepository.GetUserCurrentAddressAsync(address.UserId);


        if (address == null)
            return ChangeCurrentAddressResult.AddressNotFound;

        try
        {
            if (oldAddress != null)
            {
                oldAddress.CurrentAddress = false;
                _userAddressRepository.UpdateUserAddress(oldAddress);
            }

            address.CurrentAddress = true;
            _userAddressRepository.UpdateUserAddress(address);
            await _userAddressRepository.SaveAsync();
            return ChangeCurrentAddressResult.Success;
        }
        catch
        {
            return ChangeCurrentAddressResult.Error;
        }
    }

    #endregion
}