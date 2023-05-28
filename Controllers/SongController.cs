using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL3.Models.Domain;
using PBL3.Models.DTO;
using PBL3.Repositories.Abstract;
using PBL3.Repositories.Implementation;
using System.Xml.Linq;

namespace PBL3.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class SongController : Controller
    {
        private readonly ISongRepository songRepository;
        private readonly IRepository<Genre> genreRepository;
        private readonly IRepository<Emotion> emotionRepository;

        public SongController(ISongRepository songService, IRepository<Genre> genreService
            , IRepository<Emotion> emotionService) 
        {
            this.songRepository = songService;
            this.genreRepository = genreService;
            this.emotionRepository = emotionService;
        }
        public IActionResult Index()
        {
            var listSong = songRepository.GetAllWithRelatedGenreAndEmotion()
                .Select(s => new ListSongItem()
                {
                    Song = s,
                    GenreNames = string.Join(",", s.Genres.Select(g => g.Name)),
                    EmotionNames = string.Join(",", s.Emotions.Select(e => e.Name))
                });
            return View(listSong);
        }
        private void AddSelectListToView()
        {
            var emotions = emotionRepository.GetAll().Select(e => new SelectListItem()
                { Text = e.Name, Value = e.EmotionId.ToString() });
            var genres = genreRepository.GetAll().Select(g => new SelectListItem()
                { Text = g.Name, Value = g.GenreId.ToString() });
            ViewBag.Emotions = emotions;
            ViewBag.Genres = genres;
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddSelectListToView();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSongModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate hợp lệ, thực hiện thêm Song
                Song song = new Song()
                {
                    Name = model.Name!,
                    Author = model.Author!,
                    Singer = model.Singer!,
                    Source = model.Source!
                };
                // Set relationship
				songRepository.SetRelatedEmotion(song, model.EmotionIds!);
				songRepository.SetRelatedGenre(song, model.GenreIds!);
                // Add
				songRepository.Add(song);

                return RedirectToAction(nameof(Index));
            }
            AddSelectListToView();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Song? song = songRepository.GetByIdWithRelatedEntity(id);
            if (song == null) return NotFound();

            EditSongModel viewModel = new EditSongModel()
            {
                SongId = song.SongId,
                Name = song.Name,
                Author = song.Author,
                Singer = song.Singer,
                Source = song.Source
            };
            viewModel.EmotionIds = song.Emotions.Select(e => e.EmotionId);
			viewModel.GenreIds = song.Genres.Select(g => g.GenreId);

			AddSelectListToView();
			return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(EditSongModel model)
        {
            if (ModelState.IsValid)
            {
                Song? song = songRepository.GetByIdWithRelatedEntity(model.SongId);
                if (song == null) return NotFound();

                // set
                song.Name = model.Name!;
                song.Author = model.Author!;
                song.Singer = model.Singer!;
                song.Source = model.Source!;
                // set relationship
                songRepository.SetRelatedEmotion(song, model.EmotionIds!);
                songRepository.SetRelatedGenre(song, model.GenreIds!);
                // update
                songRepository.Update(song);

				return RedirectToAction(nameof(Index));
			}
			AddSelectListToView();
			return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            songRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

