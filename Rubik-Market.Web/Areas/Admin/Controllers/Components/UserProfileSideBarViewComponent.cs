using Microsoft.AspNetCore.Mvc;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.ViewModels.Admin.User;

namespace Rubik_Market.Web.Areas.Admin.Controllers.Components
{
    public class UserProfileSideBarViewComponent(IUserRepository userRepository):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var user = userRepository.GetUserById(id);
            UserCompoViewModel model = new UserCompoViewModel
            {
                UserId = user.ID,
                FullName = user.FullName,
                isActive = user.isActive,
                isAdmin = user.isAdmin
            };
            return View(model);
        }
    }
}
