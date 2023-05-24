using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;
using PBL3.Infrastructures;
using System.Diagnostics;
using System.Text.Json.Serialization.Metadata;

namespace PBL3.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public AccountController(IUserService userService) 
        {
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult res = await userService.LoginAsync(loginModel.Email, loginModel.Password);
                if (res.Succeeded)
                    return Redirect(loginModel.ReturnUrl);
                ModelState.AddModelError(res);
            }
            return View(loginModel);
        }
        
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register() => View();
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                // Tạo user mới
                AppUser newUser = new AppUser()
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    Birthday = registerModel.BirthDay,
                    Gender = registerModel.Gender
                };
                // Đăng kí user mới với role Role.Member
                var res = await userService.Register(newUser, registerModel.Password!);
                if (res.Succeeded)
                {
                    // Chuyển hướng sang trang đăng nhập
                    return RedirectToAction(nameof(Login));
                }
                ModelState.AddModelError(res);
            }
            return View(registerModel);
        }

        [HttpGet]
        public IActionResult ChangePassword() => View();
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult res = await userService.ChangePasswordAsync(User
                    , model.CurrentPassword, model.NewPassword);
                if (res.Succeeded)
                {
                    // Tạm thời sau khi change password thành công sẽ chuyển hướng sang /Home/Index
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(res);
                }
            }
            return View(model);
        }
        [Route("[controller]/Manage/Profile")]
        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            AppUser user = await userService.GetUserAsync(User);
            return View(new ManageProfileModel()
            {
                Account = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Age = (DateTime.Now.Year - user.Birthday?.Year),
                Birthday = user.Birthday,
                Gender = user.Gender
            });
        }
        [Route("[controller]/Manage/Profile")]
        [HttpPost]
        public async Task<IActionResult> ManageProfile(ManageProfileModel model)
        {
            if (ModelState.IsValid) 
            {
                AppUser user = await userService.GetUserAsync(User);

                // Set
                user.Name = model.Name;
                user.Birthday = model.Birthday;
                user.Gender = model.Gender;
                user.PhoneNumber = model.PhoneNumber;

                if (model.NewPassword != null)
                {
                    // Thay doi mat khau

                }

                // Update lại trong CSDL
                IdentityResult res = await userService.UpdateUserAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(ManageProfile));
                }
                ModelState.AddModelError(res);
            }
            return View(model);
        }
    }
}
