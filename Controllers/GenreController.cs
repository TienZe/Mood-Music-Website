using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;
using PBL3.Repositories.Implementation;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class GenreController : Controller
    {
        private readonly IRepository<Genre> repository;
        private const int PageSize = 10;
        public GenreController(IRepository<Genre> repoService)
        {
            repository = repoService;
        }
        public IActionResult Index(int? pageIndex, string searchString)
        {
            // Server validation
            pageIndex = (pageIndex == null || pageIndex < 1) ? 1 : pageIndex;

            var listGenres = repository.GetAll();

            // Tiếp tục mở rộng truy vấn
            if (!String.IsNullOrEmpty(searchString))
            {
                // Thực hiện Search
                listGenres = listGenres.Where(g => g.Name.Contains(searchString));
            }
            // Truyền filter hiện tại sang cho View
            ViewData["SearchString"] = searchString ?? string.Empty;

            // Sắp xếp
            listGenres = listGenres.OrderBy(g => g.GenreId);

            // Phân trang kết quả
            return View(PaginatedList<Genre>.CreateAsync(listGenres, pageIndex.Value, PageSize));
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
                Genre newGenre = new Genre { Name = model.Name };
                repository.Add(newGenre);
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
                Genre? genre = repository.GetById(model.GenreId);
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
