using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.RegisterUserAsync(user);
                return PartialView("Login");
            }
            return PartialView(user);
        }

        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.LoginUserAsync(username, password);
            if (user != null)
 
            {
                if (user.IsAdmin)
                {
                    return PartialView("AdminDashboard", "Employee");
                }
                else
                {
                    return PartialView("UserDashboard", "Employee");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return PartialView();
        }

        public IActionResult Logout()
        {
            // Clear the user session or authentication cookie
            return RedirectToAction("Index", "Home");
        }
    }
}