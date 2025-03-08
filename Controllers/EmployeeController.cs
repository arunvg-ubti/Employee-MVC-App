using EmployeeManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult AdminDashboard()
    {
        return View();
    }

    public IActionResult UserDashboard()
    {
        return View();
    }

    public IActionResult AddEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(Employee employee)
    {
        ViewData["ShowDashboardLink"] = true;
        if (ModelState.IsValid)
        {
            var employeeOperations = new EmployeeOperations(_employeeService);
            await employeeOperations.AddEmployeeAsync(employee);
            ViewBag.Message = "Employee added successfully!";
            return View(employee);
        }
        return View(employee);
    }

    public IActionResult UpdateEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmployee(Employee employee)
    {
        ViewData["ShowDashboardLink"] = true;
        if (ModelState.IsValid)
        {
            var employeeOperations = new EmployeeOperations(_employeeService);
            await employeeOperations.UpdateEmployeeAsync(employee);
            ViewBag.Message = "Employee updated successfully!";
            return View(employee);
        }
        return View(employee);
    }

    public IActionResult DeleteEmployee()
    {
        ViewData["ShowDashboardLink"] = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        ViewData["ShowDashboardLink"] = true;
        var employeeOperations = new EmployeeOperations(_employeeService);
        await employeeOperations.DeleteEmployeeAsync(id);
        ViewBag.Message = "Employee deleted successfully!";
        return View();
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
        var employeeOperations = new EmployeeOperations(_employeeService);
    
        Employee employee = null;
        if (!string.IsNullOrEmpty(searchQuery))
        {
            employee = await employeeOperations.SearchEmployeeByIdOrNameAsync(searchQuery);
        }
    
        return View(employee);
    }

    public async Task<IActionResult> DisplayAllEmployees()
    {
        ViewData["ShowDashboardLink"] = true;
        var employeeOperations = new EmployeeOperations(_employeeService);
        var employees = await employeeOperations.DisplayAllEmployeesAsync();
        return View(employees);
    }
}