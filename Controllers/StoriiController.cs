using Microsoft.AspNetCore.Mvc;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Controllers
{
    public class StoriiController : Controller
    {
        private readonly IRepository<Story> storyRepository;
        public StoriiController(IRepository<Story> storyService)
        {
            this.storyRepository = storyService;
        }
        public IActionResult Index()
        {
            return View(storyRepository.GetAll());
        }
        public IActionResult ListenStorii() => View();
    }
}
