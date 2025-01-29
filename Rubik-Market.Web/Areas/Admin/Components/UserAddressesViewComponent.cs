using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;

namespace Rubik_Market.Web.Areas.Admin.Components;

public class UserAddressesViewComponent(IUserAddressServices userAddressServices):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var address = await userAddressServices.GetUserAddressesAdminSideAsync(userId);
        return View("UserAddresses", address);
    }
}