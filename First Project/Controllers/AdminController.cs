using First_Project.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace First_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.numOfUsers=_context.Logins.Where(x=>x.RoleId==3).Count();
            ViewBag.numberofChef=_context.Logins.Where(x=>x.RoleId ==2).Count();
            ViewBag.numberOfRecipes=_context.Recipes.Count();
            ViewBag.NumberOfCategories=_context.Categories.Count();

            var id = HttpContext.Session.GetInt32("AdminId");
            var user=_context.Users.Where(x=>x.UserId == id).FirstOrDefault();
            return View(user);
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","LoginAndRegister");
        }
    }
}
