using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChooseRole()
        {
            return View();
        }

        public IActionResult AdminOptions()
        {
            TempData["Role"] = "Admin";
            return View();
        }

        public IActionResult UserOptions()
        {
            TempData["Role"] = "User";
            return View();
        }

        public IActionResult AdminRegister()
        {
            TempData["Role"] = "Admin";
            return RedirectToAction("Register", "Account");
        }

        public IActionResult AdminLogin()
        {
            TempData["Role"] = "Admin";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult UserRegister()
        {
            TempData["Role"] = "User";
            return RedirectToAction("Register", "Account");
        }

        public IActionResult UserLogin()
        {
            TempData["Role"] = "User";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ToDo()
        {
            return View();
        }
    }
}