using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;

namespace First_Project.Controllers
{
    public class UserRecipesController : Controller
    {
        private readonly ModelContext _context;

        public UserRecipesController(ModelContext context)
        {
            _context = context;
        }

        // GET: UserRecipes
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.UserRecipes.Include(u => u.Recipe).Include(u => u.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: UserRecipes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.UserRecipes == null)
            {
                return NotFound();
            }

            var userRecipe = await _context.UserRecipes
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRecipe == null)
            {
                return NotFound();
            }

            return View(userRecipe);
        }

        // GET: UserRecipes/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserRecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,PurchaseDate,UserId,RecipeId")] UserRecipe userRecipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", userRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userRecipe.UserId);
            return View(userRecipe);
        }

        // GET: UserRecipes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.UserRecipes == null)
            {
                return NotFound();
            }

            var userRecipe = await _context.UserRecipes.FindAsync(id);
            if (userRecipe == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", userRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userRecipe.UserId);
            return View(userRecipe);
        }

        // POST: UserRecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Quantity,PurchaseDate,UserId,RecipeId")] UserRecipe userRecipe)
        {
            if (id != userRecipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRecipeExists(userRecipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", userRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userRecipe.UserId);
            return View(userRecipe);
        }

        // GET: UserRecipes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.UserRecipes == null)
            {
                return NotFound();
            }

            var userRecipe = await _context.UserRecipes
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRecipe == null)
            {
                return NotFound();
            }

            return View(userRecipe);
        }

        // POST: UserRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.UserRecipes == null)
            {
                return Problem("Entity set 'ModelContext.UserRecipes'  is null.");
            }
            var userRecipe = await _context.UserRecipes.FindAsync(id);
            if (userRecipe != null)
            {
                _context.UserRecipes.Remove(userRecipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRecipeExists(decimal id)
        {
          return (_context.UserRecipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
