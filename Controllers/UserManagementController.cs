using Microsoft.AspNetCore.Mvc;

namespace PBL3.Controllers
{
    public class UserManagementController : Controller
    {
        public IActionResult UserManagement()
        {
            return View();
        }
    }
}
