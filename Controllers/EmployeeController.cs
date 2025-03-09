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
 
    public IActionResult AdminDashboard() => View();
    public IActionResult UserDashboard() => View();
 
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
 
        // 🔍 DEBUG: Log validation errors
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            TempData["Error"] = "Validation failed: " + string.Join(", ", errors);
            return View(employee);
        }
 
        // 🔍 DEBUG: Ensure Employee ID is entered
        if (string.IsNullOrEmpty(employee.Id))
        {
            TempData["Error"] = "Employee ID is required!";
            return View(employee);
        }
 
        await _employeeService.UpdateEmployeeAsync(employee);
        TempData["UpdateMessage"] = "Employee updated successfully!";
        
        return RedirectToAction(nameof(DisplayAllEmployees));  // ✅ Ensures redirection
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