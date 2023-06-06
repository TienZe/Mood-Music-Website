using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    public class StoryController : Controller
    {
        private readonly IRepository<Story> repository;
        private const int PageSize = 4;
        public StoryController(IRepository<Story> repoService)
        {
            repository = repoService;
        }
        public IActionResult Index(int? pageIndex, string searchString)
        {
            // Server validation
            pageIndex = (pageIndex == null || pageIndex < 1) ? 1 : pageIndex;

            var listStories = repository.GetAll();

            // Tiếp tục mở rộng truy vấn
            if (!String.IsNullOrEmpty(searchString))
            {
                // Thực hiện Search
                listStories = listStories.Where(s => s.Name.Contains(searchString));
            }
            // Truyền filter hiện tại sang cho View
            ViewBag.SearchString = searchString ?? string.Empty;

            // Sắp xếp
            listStories = listStories.OrderBy(s => s.StoryId);

            // Phân trang kết quả
            return View(PaginatedList<Story>.CreateAsync(listStories, pageIndex.Value, PageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Story model)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Story? story = repository.GetById(id);
            return View(story);
        }
        [HttpPost]
        public IActionResult Edit(Genre model)
        {
            if (ModelState.IsValid)
            {
                Story? story = repository.GetById(model.GenreId);
                if (story != null)
                {
                    // Cập nhật lại story
                    
                }
                ModelState.AddModelError(string.Empty, "Something went wrong, not found specified genre");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
