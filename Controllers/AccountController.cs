using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
 
namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
 
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
 
        public IActionResult Register()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.IsAdmin = TempData["Role"]?.ToString() == "Admin";
 
                try
                {
                    await _userService.RegisterUserAsync(user);
                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login");
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError("", "Database error: " + dbEx.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred: " + ex.Message);
                }
            }
 
            return View(user);
        }
 
        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View();
            }
 
            var user = await _userService.LoginUserAsync(username, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.IsAdmin ? "Admin" : "User");
                return RedirectToAction(user.IsAdmin ? "AdminDashboard" : "UserDashboard", "Employee");
            }
 
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
 