using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBL3.Models;
using System.Diagnostics;

namespace PBL3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Storii() => View();
        public IActionResult EmotionSong() => View();
    }
}