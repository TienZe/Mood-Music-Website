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

        public IActionResult AddNewSong() => View();
    }
}
