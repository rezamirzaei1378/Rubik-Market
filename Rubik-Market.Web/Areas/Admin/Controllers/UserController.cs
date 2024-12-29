using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubik_Market.Application.Extenstions;
using Rubik_Market.Application.Services.Contracts;
using Rubik_Market.Domain.Repo.Contracts;
using Rubik_Market.Domain.Models;
using Rubik_Market.Domain.ViewModels.Admin.User;
using Rubik_Market.Infra.IOC.Context;

namespace Rubik_Market.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {


        #region Costructor
        private readonly IUserRepository _userRepository;
        private readonly IUserPersonalInfoRepository _userPersonalInfoRepository;

        public UserController(IUserRepository userRepository,IUserPersonalInfoRepository userPersonalInfoRepository)
        {
            _userRepository = userRepository;
            _userPersonalInfoRepository = userPersonalInfoRepository;
        }

        #endregion

        #region UserList

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }

        #endregion

        #region CreateUser

        [HttpGet("Create-User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create-User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            try
            {
                if (_userRepository.IsUserExistByEmailAsync(model.UserEmail.Trim().ToLower()))
                {
                    ModelState.AddModelError(nameof(CreateUserViewModel.UserEmail), "کاربری با این ایمیل موجو است");
                    return View(model);
                }

                User user = new User()
                {
                    FullName = model.FullName,
                    Email = model.UserEmail,
                    CreateDate = DateTime.Now,
                    ConfirmCode = null,
                    Password = model.Password.EncodePasswordMd5(),
                    isAdmin = model.isAdmin,
                    isActive = model.isActive,
                    isDelete = false
                };
                //TODO Add user image
                await _userRepository.AddUserAsync(user);
                await _userRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }

        }

        #endregion

        #region EditUser

        [HttpGet("Edit-User")]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel()
            {
                FullName = user.FullName,
                Id = user.ID,
                isActive = user.isActive,
                isAdmin = user.isAdmin,
                UserEmail = user.Email,
            };
            return View(model);
        }
        [HttpPost("Edit-User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            try
            {
                var userPassword = _userRepository.GetUserById(model.Id).Password;
                User user2 = new User()
                {
                    ID = model.Id,
                    FullName = model.FullName,
                    Email = model.UserEmail,
                    isAdmin = model.isAdmin,
                    isActive = model.isActive,
                    Password = userPassword
                };
                _userRepository.UpdateUser(user2);
                await _userRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userRepository.IsUserExistByIdAsync(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region DeleteUser

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            user.isDelete = true;
            _userRepository.DeleteUser(user);
            await _userRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region User Detailes

        public IActionResult UserDetail(int id)
        {
            var user = _userRepository.GetUserById(id);
            var userProfile = _userPersonalInfoRepository.GetUserPersonalInfo(id);
            if (user==null)
            {
                NotFound();
            }

            UserDetailViewModel model = new UserDetailViewModel
            {
                UserId = user.ID,
                Email = user.Email,
                isDeleted = user.isDelete,
                UserCreateDate = user.CreateDate
            };
            if (userProfile == null)
            {
                model.CardNumberForRejectMoney = " ";
                model.NationalCode = " ";
                model.BirthDate = " ";
                model.CellPhoneNumber = " ";
                model.HousePhoneNumber = " ";
            }
            else
            {
                model.NationalCode = userProfile.NationalCode ?? " ";
                model.HousePhoneNumber = userProfile.HousePhoneNumber ?? " ";
                model.BirthDate = (userProfile.BirthDate==null) ? " " : userProfile.BirthDate.ToShamsiStr();
                model.CardNumberForRejectMoney = (userProfile.CardNumberForRejectMoney==null)? " ":userProfile.CardNumberForRejectMoney.ChangeCardShowFormat();
                model.CellPhoneNumber = userProfile.CellPhoneNumber ?? " ";
            }

            return View(model);
        }

        #endregion

        #region User Detailes Add

        [HttpGet]
        public IActionResult UserDetailAdd(int id)
        {
            ViewData["UserId"] = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserDetailAdd(AddUserDetailViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            try
            {
                UserProfileInfo profile = new UserProfileInfo
                {
                    CreateDate = DateTime.Now,
                    isDelete = false,
                    UserId = model.UserId,
                    NationalCode = model.NationalCode,
                    CellPhoneNumber = model.CellPhoneNumber,
                    HousePhoneNumber = model.HousePhoneNumber,
                    BirthDate = null,
                    CardNumberForRejectMoney = model.CardNumberForRejectMoney,
                };
                _userPersonalInfoRepository.AddUserPersonalInfo(profile);
                await _userPersonalInfoRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        #endregion

        #region User Detaile Edit

        public IActionResult UserDetailEdit()
        {
            return View();
        }

        #endregion

        #region User Detaile Delete

        public async Task<IActionResult> UserDetailDelete()
        {
            return View();
        }

        #endregion
    }
}
