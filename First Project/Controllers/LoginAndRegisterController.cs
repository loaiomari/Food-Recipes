using First_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly ModelContext _context;

        public LoginAndRegisterController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            ViewData["Role"] = new SelectList(_context.Roles.Except(_context.Roles.Where(x=>x.RoleId==1)), "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user,string userName ,string Password ,int Role)
        {
            
                _context.Add(user);
                _context.SaveChanges();

                Login userlogin=new Login();
                userlogin.UserName = userName;
                userlogin.Password = Password;
                userlogin.UserId = user.UserId;
                userlogin.RoleId = Role;

                _context.Add(userlogin);
                _context.SaveChanges();
                return RedirectToAction("Login", "LoginAndRegister");

            

            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user=await _context.Logins.Where(x=>x.UserName==login.UserName && x.Password==login.Password).SingleOrDefaultAsync();
            if(user==null)
            {
                throw new Exception("username or password incorrect ");
            }
            switch(user.RoleId)
            {
                case 1:
                    HttpContext.Session.SetInt32("AdminId", (int)user.UserId);
                    return RedirectToAction("Index", "Admin");

                case 2:
                    HttpContext.Session.SetInt32("ChefID",(int)user.UserId);
                    return RedirectToAction("Profile", "Chef");

                case 3:
                    HttpContext.Session.SetInt32("UserID",(int)(user.UserId));
                    return RedirectToAction("Index", "User");
            }
            return View();
        }
       
    }
}
