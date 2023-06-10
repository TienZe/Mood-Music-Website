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
        private readonly IUserRepository userRepository;
        public AccountController(IUserService userService, IUserRepository userRepoService) 
        {
            this.userService = userService;
            this.userRepository = userRepoService;
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

        [Route("[controller]/Manage/Profile")]
        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            AppUser user = await userService.GetUserAsync(User);

			ViewData["Point"] = user.Point;
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
			// Get user
			AppUser user = await userService.GetUserAsync(User);
			if (ModelState.IsValid) 
            {
				// Thay đổi mật khẩu, gần hơn là Reset mật khẩu vì không yêu cầu ng dùng
				// nhập lại mật khẩu
				if (model.NewPassword != null)
				{
					// Thay doi mat khau
					IdentityResult resetPasswordRes = await userService.ResetPasswordAsync(User
						, model.NewPassword);
					if (!resetPasswordRes.Succeeded)
                    {
						ModelState.AddModelError(resetPasswordRes);
                        return View(model);
					}
				}

				// Set
				user.Name = model.Name;
				user.Birthday = model.Birthday;
				user.Gender = model.Gender;
				user.PhoneNumber = model.PhoneNumber;

				// Update lại trong CSDL
				IdentityResult res = await userService.UpdateUserAsync(user);
				if (!res.Succeeded)
				{
					ModelState.AddModelError(res);
                    return View(model);
				}
                // Thành công
				TempData["Message"] = "Your changes have been updated";
				return RedirectToAction(nameof(ManageProfile));
			}

			ViewData["Point"] = user.Point;
			return View(model);
        }

        public async Task<IActionResult> ManageStories()
        {
            AppUser user = await userService.GetUserAsync(User);
            userRepository.LoadRelatedStories(user);
            ViewData["Point"] = user.Point;
            return View(user.Stories);
        }
    }
}
