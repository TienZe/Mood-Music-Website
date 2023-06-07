using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize(Roles = Models.Domain.Role.Admin)]
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

        public IActionResult MemberDetail() => View();
        public IActionResult ListSongUsing() => View();
        public IActionResult AddNewSong() => View();
        public IActionResult SongEditting() => View();
        public IActionResult ListGenresOfSong() => View();
        public IActionResult AddNewGenre() => View();
        public IActionResult GenreEditting() => View();
        public IActionResult ListEmotions() => View();
        public IActionResult AddNewEmotion() => View();
        public IActionResult EmotionEditting() => View();
        public IActionResult Order() => View();
        public IActionResult OrderDetails() => View();
        public IActionResult ListStory() => View();
        public IActionResult AddNewStory() => View();
        public IActionResult StoryEditting() => View();
    }
}
