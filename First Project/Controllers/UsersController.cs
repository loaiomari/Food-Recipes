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
    public class UsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Fname,Lname,Email,ImageFile")] User user)
        {
            if (ModelState.IsValid)
            {
                if(user.ImageFile !=null)
                {
                    string wwwrootPath=_webHostEnvironment.WebRootPath;
                    string ImageName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string fullPath=Path.Combine(wwwrootPath +"/Image/", ImageName);
                    using(var fileStream=new FileStream(fullPath,FileMode.Create))
                    {
                        user.ImageFile.CopyToAsync(fileStream);
                    }
                    user.ImageName = ImageName;

                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id, [Bind("UserId,Fname,Lname,Email,ImageFile")] User users)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
           
            if (user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (users.ImageFile != null && users.ImageFile.Length > 0)
                {
                    try
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string ImageName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(users.ImageFile.FileName);
                        string fullPath = Path.Combine(wwwrootPath, "Image", ImageName);

                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await users.ImageFile.CopyToAsync(fileStream);
                        }

                        user.ImageName = ImageName;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it appropriately
                        ModelState.AddModelError("", "Unable to upload image.");
                        // You may return the view with an error message here
                    }
                }

                // Update other user properties and save changes to database
                user.Fname = users.Fname;
                user.Lname = users.Lname;
                user.Email = users.Email;

                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency conflict
                    ModelState.AddModelError("", "Concurrency conflict occurred.");
                }
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,Fname,Lname,Email,ImageName")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ModelContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(decimal id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
