using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class GenreController : Controller
    {
        private readonly IRepository<Genre> repository;
        public GenreController(IRepository<Genre> repoService)
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
        public IActionResult Create(Genre model)
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
            Genre? genre = repository.GetById(id);
            return View(genre);
        }
        [HttpPost]
        public IActionResult Edit(Genre model)
        {
            if (ModelState.IsValid)
            {
                Genre? genre = repository.AsQueryable().FirstOrDefault(g => g.GenreId == model.GenreId);
                if (genre != null)
                {
                    // Cập nhật lại genre, hiện tại chỉ cập nhật Name của Genre
                    genre.Name = model.Name;
                    repository.Update(genre);
                    return RedirectToAction(nameof(Index));
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
