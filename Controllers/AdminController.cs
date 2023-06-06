using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private const int PageSize = 4;
        public AdminController(IUserService userService) 
        {
            this.userService = userService;
        }
        
        public async Task<IActionResult> ListMembers(int? pageIndex, string searchString)
        {
            // Server validation
            pageIndex = (pageIndex == null || pageIndex < 1) ? 1 : pageIndex;

            IEnumerable<AppUser> listMembers = await userService.GetUsersInRoleAsync(Role.Member);
            // Tiếp tục lọc (in-memory)
            if (!String.IsNullOrEmpty(searchString))
            {
                // Thực hiện filter theo emotion name
                listMembers = listMembers.Where(member => (member.Name?.Contains(searchString) ?? false) 
                    || member.Email.Contains(searchString));
            }
            // Truyền filter hiện tại sang cho View
            ViewBag.SearchString = searchString ?? string.Empty;

            // Sắp xếp
            listMembers = listMembers.OrderBy(member => member.Id);

            // Phân trang kết quả
            return View(PaginatedList<AppUser>.CreateAsync(listMembers, pageIndex.Value, PageSize));
        }
        
        public IActionResult Order() => View();
        public IActionResult OrderDetails() => View();
    }
}
