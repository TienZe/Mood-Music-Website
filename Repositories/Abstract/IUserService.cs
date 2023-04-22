using Microsoft.AspNetCore.Identity;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using System.Security.Claims;

namespace PBL3.Repositories.Abstract
{
    public interface IUserService
    {
        Task<AppUser> GetUserAsync(ClaimsPrincipal user);
        Task<IdentityResult> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<IdentityResult> Register(RegisterModel model);
        Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword
            , string newPassword);
        Task<IdentityResult> UpdateUserAsync(AppUser user);
    }
}
