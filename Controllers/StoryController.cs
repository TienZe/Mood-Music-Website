using Microsoft.AspNetCore.Mvc;

namespace PBL3.Controllers
{
    public class StoryController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
        public IActionResult Edit() => View();
        public IActionResult Details() => View();
    }
}
