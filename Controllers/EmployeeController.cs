using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using System.Threading.Tasks;
using System.Linq;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult AdminDashboard()
    {
        var token = HttpContext.Session.GetString("JWToken");
        Console.WriteLine($"DEBUG: AdminDashboard JWToken: '{token}'"); // Debug log
        ViewBag.JwtToken = token;
        return View();
    }

    public IActionResult UserDashboard()
    {
        var token = HttpContext.Session.GetString("JWToken");
        Console.WriteLine($"DEBUG: UserDashboard JWToken: '{token}'"); // Debug log
        ViewBag.JwtToken = token;
        return View();
    }

    public IActionResult AddEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        ViewBag.Message = TempData["AddMessage"];
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(Employee employee)
    {
        ViewData["ShowDashboardLink"] = true;

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please fill in all required fields correctly.";
            return View(employee);
        }

        await _employeeService.AddEmployeeAsync(employee);
        TempData["AddMessage"] = "Employee added successfully!";
        return RedirectToAction("AddEmployee");
    }

    public IActionResult UpdateEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        ViewBag.Message = TempData["UpdateMessage"];
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmployee(Employee employee)
    {
        ViewData["ShowDashboardLink"] = true;

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
            TempData["Error"] = "Validation failed: " + string.Join(", ", errors);
            return View(employee);
        }

        if (string.IsNullOrEmpty(employee.Id))
        {
            TempData["Error"] = "Employee ID is required!";
            return View(employee);
        }

        await _employeeService.UpdateEmployeeAsync(employee);
        TempData["UpdateMessage"] = "Employee updated successfully!";
        return RedirectToAction(nameof(DisplayAllEmployees));
    }

    public IActionResult DeleteEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        ViewBag.Message = TempData["DeleteMessage"];
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        ViewData["ShowDashboardLink"] = true;

        if (string.IsNullOrEmpty(id))
        {
            TempData["Error"] = "Invalid Employee ID.";
            return View();
        }

        await _employeeService.DeleteEmployeeAsync(id);
        TempData["DeleteMessage"] = "Employee deleted successfully!";
        return RedirectToAction("DeleteEmployee");
    }

    public IActionResult SearchEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SearchEmployee(string searchQuery)
    {
        ViewData["ShowDashboardLink"] = true;

        if (string.IsNullOrEmpty(searchQuery))
        {
            TempData["Error"] = "Please enter an Employee ID or Name.";
            return View();
        }

        var employee = await _employeeService.GetEmployeeByIdOrNameAsync(searchQuery);
        if (employee == null)
        {
            TempData["Error"] = "No employee found with the given ID or Name.";
        }

        return View(employee);
    }

    public async Task<IActionResult> DisplayAllEmployees()
    {
        ViewData["ShowDashboardLink"] = true;
        return View(await _employeeService.GetAllEmployeesAsync());
    }
}