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
    public class SongController : Controller
    {
        private readonly IRepository<Song> songRepository;
        private readonly IRepository<Genre> genreRepository;
        private readonly IRepository<Emotion> emotionRepository;

        public SongController(IRepository<Song> songService, IRepository<Genre> genreService
            , IRepository<Emotion> emotionService) 
        {
            this.songRepository = songService;
            this.genreRepository = genreService;
            this.emotionRepository = emotionService;
        }
        public IActionResult Index()
        {
            var listSong = songRepository.AsQueryable().Include(s => s.Genres).Include(s => s.Emotions)
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
            if (model.GenreIds == null)
            {
                ModelState.AddModelError(nameof(model.GenreIds), "Please choose genre of this song");
            }
            if (model.EmotionIds == null)
            {
                ModelState.AddModelError(nameof(model.EmotionIds), "Please choose emotion of this song");
            }
            if (ModelState.IsValid)
            {
                // Validate hợp lệ, thực hiện thêm Song
                Song song = new Song()
                {
                    Name = model.Name,
                    Author = model.Author,
                    Singer = model.Singer,
                    Source = model.Source
                };
                foreach (int genreId in model.GenreIds)
                {
                    Genre? genre = genreRepository.GetById(genreId);
                    if (genre != null)
                    {
                        song.Genres.Add(genre);
                    }
                }
                foreach (int emotionId in model.EmotionIds)
                {
                    Emotion? emotion = emotionRepository.GetById(emotionId);
                    if (emotion != null)
                    {
                        song.Emotions.Add(emotion);
                    }
                }
                songRepository.Add(song);

                return RedirectToAction(nameof(Index));
            }
            AddSelectListToView();
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            songRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

