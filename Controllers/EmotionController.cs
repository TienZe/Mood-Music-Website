using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class EmotionController : Controller
    {
        private readonly IEmotionRepository repository;
        private const int PageSize = 4;
        public EmotionController(IEmotionRepository repoService)
        {
            repository = repoService;
        }
        public async Task<IActionResult> Index(int? pageIndex, string searchString)
        {
            // Server validation
            pageIndex = (pageIndex == null || pageIndex < 1) ? 1 : pageIndex;

            var listEmotions = repository.GetAll();

            // Tiếp tục mở rộng truy vấn
            if (!String.IsNullOrEmpty(searchString))
            {
                // Thực hiện filter theo emotion name
                listEmotions = listEmotions.Where(e => e.Name.Contains(searchString));
            }
            // Truyền filter hiện tại sang cho View
            ViewBag.SearchString = searchString ?? string.Empty;

            // Sắp xếp
            listEmotions = listEmotions.OrderBy(e => e.EmotionId);

            // Phân trang kết quả
            return View(await PaginatedList<Emotion>.CreateAsync(listEmotions, pageIndex.Value, PageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Emotion model)
        {
            if (ModelState.IsValid)
            {
                Emotion newEmotion = new Emotion { Name = model.Name };
                repository.Add(newEmotion);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Emotion? emotion = repository.GetById(id);
            return View(emotion);
        }
        [HttpPost]
        public IActionResult Edit(Emotion model)
        {
            if (ModelState.IsValid)
            {
                Emotion? emotion = repository.GetById(model.EmotionId);
                if (emotion != null)
                {
                    // Cập nhật lại genre, hiện tại chỉ cập nhật Name của Genre
                    emotion.Name = model.Name;
                    repository.Update(emotion);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Something went wrong, not found specified emotion");
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
