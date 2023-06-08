using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize]
    public class StoriiController : Controller
    {
        private readonly IRepository<Story> storyRepository;
        private readonly IRepository<OrderType> orderTypeRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserService userService;
        public StoriiController(IRepository<Story> storyService, IRepository<OrderType> orderTypeService
            , IUserService userService, IOrderRepository oderService)
        {
            this.storyRepository = storyService;
            this.orderTypeRepository = orderTypeService;
            this.userService = userService;
            this.orderRepository = oderService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(storyRepository.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> MakingOrder(int storyId) 
        {
            Story? story = storyRepository.GetById(storyId);
            if (story == null) return NotFound();

            // Lấy danh sách tên các order type
            var listOrderTypes = orderTypeRepository.GetAll().Select(ot => new SelectListItem()
                {
                    Text = ot.Name.ToString() + "purchase",
                    Value = ot.OrderTypeId.ToString()
                })
                .ToList();
            // Get user hiện tại
            AppUser user = await userService.GetUserAsync(User);
            return Json(new
            {
                Story = story,
                Day = DateTime.Now,
                OrderTypes = listOrderTypes,
                User = user.Name,
                Email = user.Email
            });
        }
        [HttpPost]
        public async Task<IActionResult> MakingOrder(int storyId, int orderTypeId) 
        {
            // Get story được order
            Story? story = storyRepository.GetById(storyId);
            // Get orderType được chọn
            OrderType? orderType = orderTypeRepository.GetById(orderTypeId);
            // Get user hiện tại
            AppUser user = await userService.GetUserAsync(User);

            if (story == null || orderType == null || user == null) return BadRequest();

            Order newOrder = new Order()
            {
                Name = story.Name + "-" + orderType.Name.ToString(),
                Price = (orderType.Name == OrderType.Type.Onetime ? story.OneTimePrice : story.LifeTimePrice),
                Day = DateTime.Now,
                User = user,
                OrderType = orderType,
                Story = story
            };
            orderRepository.Add(newOrder);

            // Order thành công, chuyển hướng sang nghe story
            return RedirectToAction(nameof(Index));
        }
    }
}
