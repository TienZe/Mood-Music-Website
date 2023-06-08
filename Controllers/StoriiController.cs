using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;
using System.Linq;

namespace PBL3.Controllers
{
    [Authorize]
    public class StoriiController : Controller
    {
        private readonly IRepository<Story> storyRepository;
        private readonly IRepository<OrderType> orderTypeRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        public StoriiController(IRepository<Story> storyService, IRepository<OrderType> orderTypeService
            , IUserService userService, IOrderRepository oderService, IUserRepository userRepoService)
        {
            this.storyRepository = storyService;
            this.orderTypeRepository = orderTypeService;
            this.userService = userService;
            this.orderRepository = oderService;
            this.userRepository = userRepoService;
        }
        private void AddSelectListOrderType()
        {
            // Lấy danh sách tên các order type
            var listOrderTypes = orderTypeRepository.GetAll().Select(ot => new SelectListItem()
            {
                Text = ot.Name.ToString() + " purchase",
                Value = ot.OrderTypeId.ToString()
            })
                .ToList();
            ViewBag.SelectListOrderType = listOrderTypes;
        }

        // Dựa vào tất cả stories trong CSDL và stories của User hiện tại
        // để trả về ds các StoryItem bao gồm thông tin Story và thông tin
        // đã được mua bởi user hiện tại chưa
        private async Task<IEnumerable<StoryItem>> GetListStoryItem()
        {
            var stories = storyRepository.GetAll();
           
            AppUser? user = await userService.GetUserAsync(User);
            IEnumerable<int> listUserStories = new List<int>();
            if (user != null)
            {
                userRepository.LoadRelatedStories(user);
                listUserStories = user.Stories.Select(story => story.StoryId);
            }
            var listItem = stories.Select(story => new StoryItem()
            {
                Story = story,
                AlreadyBought = listUserStories.Contains(story.StoryId)
            }).ToList();
            return listItem;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
			AddSelectListOrderType();
            return View(await GetListStoryItem());
        }
        [HttpGet]
        public async Task<IActionResult> OrderStory(int storyId) 
        {
            Story? story = storyRepository.GetById(storyId);
            if (story == null) return NotFound();

            // Lấy danh sách tên các order type
            var listOrderTypes = orderTypeRepository.GetAll().Select(ot => new SelectListItem()
                {
                    Text = ot.Name.ToString() + " purchase",
                    Value = ot.OrderTypeId.ToString()
                })
                .ToList();
            // Get user hiện tại
            AppUser user = await userService.GetUserAsync(User);
            return Json(new
            {
                Story = story,
                Day = DateTime.Now.ToString("yyyy-MM-dd"),
                OrderTypes = listOrderTypes,
                User = user.Name,
                UserAccount = user.Email
            });
        }
        [HttpPost]
        public async Task<IActionResult> MakingOrder(int? storyId, int? orderTypeId) 
        {
            if (storyId == null || orderTypeId == null)
            {
                // Đã validate ở client, ở server tạm thời khi lỗi sẽ không làm gì cả
                return RedirectToAction(nameof(Index));
            }

            // Get story được order
            Story? story = storyRepository.GetById(storyId.Value);
            // Get orderType được chọn
            OrderType? orderType = orderTypeRepository.GetById(orderTypeId.Value);
            // Get user hiện tại
            AppUser user = await userService.GetUserAsync(User);

            if (story == null || orderType == null || user == null)
            {
				return BadRequest();
			}

            Order newOrder = new Order()
            {
                Name = story.Name + " - " + orderType.Name.ToString(),
                Price = (orderType.Name == OrderType.Type.Onetime ? story.OneTimePrice 
                            : story.LifeTimePrice),
                Day = DateTime.Now,
                User = user,
                OrderType = orderType,
                Story = story
            };
            orderRepository.Add(newOrder);

            // Check order type, nếu là lifetime thì cần thêm quan hệ giữa user và story
            if (orderType.Name == OrderType.Type.Lifetime)
            {
                userRepository.AddRelatedStory(user, story);
            }

            // Đánh dấu người dùng ở phiên hiện tại order thành công và sẵn sàng để
            // nghe story có id storyId
            TempData["OrderSuccess"] = true;
            // Order thành công, chuyển hướng sang nghe story
            return RedirectToAction(nameof(ListenStory), new { storyId = storyId });
        }
        [HttpGet]
        public async Task<IActionResult> ListenStory(int? storyId)
        {
            if (storyId == null) return BadRequest();

            Story? story = storyRepository.GetById(storyId.Value);
            if (story == null) return NotFound();

            // Check lại, thật sự rằng User hiện tại đã order chưa
            bool alreadyOrder = (bool?)TempData["OrderSuccess"] ?? false;

            if (!alreadyOrder)
            {
				// Cần check thêm liệu user đã sở hữu story này chưa
				AppUser user = await userService.GetUserAsync(User);
				userRepository.LoadRelatedStories(user);
				bool alreadyBought = user.Stories.Any(story => story.StoryId == storyId);
                if (!alreadyBought) 
                {
                    // Không sở hữu, không order mà vào nghe -> truy cập lỗi
                    return BadRequest();
                }
			}

            AddSelectListOrderType();
            return View(new ListenStoryModel()
            {
                Story = story,
                StoryItems = await GetListStoryItem()
            });
        }
    }
}
