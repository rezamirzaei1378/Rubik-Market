using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.ViewModels.User.Areas;

namespace Rubik_Market.Web.Areas.Admin.Components
{
    public class UserProfileSideBarViewComponent(IUserServices userServices, IUserProfileServices userProfileServices) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = await userServices.GetUserByIdAsync(id);
            var profile = await userProfileServices.IsUserHaveProfileAsync(id);
            if (user == null)
            {
                return null;
            }

            UserComponentViewModel model = new UserComponentViewModel
            {
                UserId = user.ID,
                FullName = user.FullName,
                isActive = user.isActive,
                isAdmin = user.isAdmin,
                ProfileIsForEdit = profile != true ? "Add" : "Edit"
            };
            return View(model);
        }
    }
}
