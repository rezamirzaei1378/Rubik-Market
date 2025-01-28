using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Domain.ViewModels.Address;

namespace Rubik_Market.Web.Areas.UserPanel.Components;

public class AddUserAddressViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        AddAddressViewModel address = new AddAddressViewModel();
        address.UserId = userId;
        return View("AddUserAddress",address);
    }
}