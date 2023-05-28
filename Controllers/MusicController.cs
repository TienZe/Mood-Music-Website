using Microsoft.AspNetCore.Mvc;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
	public class MusicController : Controller
	{
		private readonly ISongRepository songRepository;
		public MusicController(ISongRepository songService) 
		{
			this.songRepository = songService;
		}
		public IActionResult Index()
		{
			var listSong = songRepository.GetAll().ToList();
			return View(listSong);
		}
	}
}
