using First_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly ModelContext _context;

        public UserController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories=await _context.Categories.ToListAsync();
            var id = HttpContext.Session.GetInt32("UserID");
            ViewBag.user = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
            return View(categories);
        }

        public async Task<IActionResult> GetRecipeByCategoryId(int id) 
        { 
            var Recipes= await _context.Recipes.Where(x=>x.CatId == id).ToListAsync();
            return View(Recipes);
        }

       
    }
}
