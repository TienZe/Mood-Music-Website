using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PBL3.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = Models.Domain.Role.Admin)]
        public IActionResult MemberDetail()
        {
            return View();
        }
        public IActionResult ListSongUsing() => View();
        public IActionResult AddNewSong() => View();
        public IActionResult SongEditting() => View();
        public IActionResult ListGenresOfSong() => View();
        public IActionResult AddNewGenre() => View();
        public IActionResult GenreEditting() => View();
        public IActionResult ListEmotions() => View();
        public IActionResult AddNewEmotion() => View();
        public IActionResult EmotionEditting() => View();
        public IActionResult Order() => View();
        public IActionResult OrderDetails() => View();
    }
}
