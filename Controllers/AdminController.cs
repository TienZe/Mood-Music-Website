using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;
using PBL3.Repositories.Implementation;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;
        private const int PageSize = 10;
        public AdminController(IUserService userService, IOrderRepository orderService
                , IUserRepository userRepoService) 
        {
            this.userService = userService;
            this.orderRepository = orderService;
            this.userRepository = userRepoService;
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
            ViewData["SearchString"] = searchString ?? string.Empty;

            // Load related Orders
            foreach (AppUser user in listMembers)
            {
                userRepository.LoadRelatedOrders(user);
            }
            // Sắp xếp
            listMembers = listMembers.OrderBy(member => member.Id);

            // Phân trang kết quả
            return View(PaginatedList<AppUser>.CreateAsync(listMembers, pageIndex.Value, PageSize));
        }
        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            AppUser user = await userService.FindByIdAsync(id);
            if (user == null) return NotFound();

            return Json(new
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = DateTime.Now.Year - user.Birthday?.Year,
                Gender = user.Gender.ToString(), 
                RegisterDay = user.RegisterDay.ToString("dd-MM-yyyy")
            });
        }
        public IActionResult ListOrders(int? pageIndex, string searchString)
        {
            // Server validation
            pageIndex = (pageIndex == null || pageIndex < 1) ? 1 : pageIndex;

            var listOrders = orderRepository.GetAllWithRelatedEntity();
            // Tiếp tục mở rộng truy vấn
            if (!String.IsNullOrEmpty(searchString))
            {
                // Thực hiện filter theo emotion name
                listOrders = listOrders.Where(o => o.Name.Contains(searchString));
            }
            // Truyền filter hiện tại sang cho View
            ViewData["SearchString"] = searchString ?? string.Empty;

            // Sắp xếp
            listOrders = listOrders.OrderByDescending(o => o.Day);

            // Phân trang kết quả
            return View(PaginatedList<Order>.CreateAsync(listOrders, pageIndex.Value, PageSize));
        }
        [HttpGet]
        public IActionResult OrderDetails(int orderId)
        {
            Order? order = orderRepository.GetByIdWithRelatedEntity(orderId);
            if (order == null) return NotFound();

            return View(new OrderViewModel()
            {
                OrderName = order.Name,
                Day = order.Day.ToString("yyyy-MM-dd HH:mm:ss"),
                Price = order.Price,

                User = order.User.Name,
                UserAccount = order.User.Email,
                OrderType = order.OrderType.Name.ToString(),
                StoryImage = order.Story.AvatarImage
            });
        }
    }
}
