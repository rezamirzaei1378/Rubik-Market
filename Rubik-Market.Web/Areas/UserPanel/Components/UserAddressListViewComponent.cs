using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.Address;

namespace Rubik_Market.Web.Areas.UserPanel.Components;

public class UserAddressListViewComponent(IUserAddressServices userAddressServices):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var model = await userAddressServices.GetUserAddressesAsync(userId);
        return View("UserAddressList",model);
    }
}