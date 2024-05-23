using First_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Controllers
{
    public class ChefController : Controller
    {
        private readonly ModelContext _context;

        public ChefController(ModelContext context)
        {
            _context = context;
        }

        
        // GET: Categories
        public async Task<IActionResult> AllCategory()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'ModelContext.Categories'  is null.");
        }

        public IActionResult AllChef()
        {
            var chefs = _context.Users.Include(x => x.Logins.Where(x => x.RoleId == 2));
            return View(chefs);
        }

        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("ChefID");
            var user = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
            return View(user);
            
        }

        public async Task<IActionResult> Recipe(int id)
        {
            var recipes= await _context.Recipes.Include(x=>x.UserRecipes.Where(x=>x.UserId == id)).ToListAsync();
            return View(recipes);
        }

        public IActionResult AllRecipes()
        {
            var recipes= _context.Recipes.ToList();
            return View(recipes);

        }
        //Get
        public IActionResult AddRecipe()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe([Bind("RecipeId,RecipeName,Sale,Price,CatId,CreateDate,Status,Ingredients")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", recipe.CatId);
            return View(recipe);
        }



    }
}
