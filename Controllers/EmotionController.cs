using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class EmotionController : Controller
    {
        private readonly IEmotionRepository repository;
        public EmotionController(IEmotionRepository repoService)
        {
            repository = repoService;
        }
        public IActionResult Index()
        {
            return View(repository.GetAll());
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
                repository.Add(model);
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
                Emotion? emotion = repository.AsQueryable()
                    .FirstOrDefault(e => e.EmotionId == model.EmotionId);
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
