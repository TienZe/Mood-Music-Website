using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Models.Domain;
using PBL3.Models.DTO;

namespace PBL3.Controllers
{
    public class AdminController : Controller
    {
        public readonly AppDbContext context;
        public static AppUser stateDetail;
        //[Authorize]
        public IActionResult UserDetail()
        {
            
            return View(TestDetail.Instance.GetAll());
        }

        //[HttpGet]
        //public IActionResult Detail()
        //{
        //   // if (id == String.Empty || id.Length == 0) return NotFound();

        //    //AppUser user = listuser.FirstOrDefault(x => x.Id == id);

        //    return View();
        //}

        public IActionResult Detail(string id)
        {
            stateDetail = TestDetail.Instance.GetAll().SingleOrDefault(p => p.Id == id);
            return View(stateDetail);
        }

       

        
    }
}
