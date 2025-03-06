using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeOperations = new EmployeeOperations(_employeeService);
                await employeeOperations.AddEmployeeAsync(employee);
                return RedirectToAction("AdminDashboard");
            }
            return View(employee);
        }

        public IActionResult UpdateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeOperations = new EmployeeOperations(_employeeService);
                await employeeOperations.UpdateEmployeeAsync(employee);
                return RedirectToAction("AdminDashboard");
            }
            return View(employee);
        }

        public IActionResult DeleteEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employeeOperations = new EmployeeOperations(_employeeService);
            await employeeOperations.DeleteEmployeeAsync(id);
            return RedirectToAction("AdminDashboard");
        }

        public IActionResult SearchEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchEmployee(string id)
        {
            var employeeOperations = new EmployeeOperations(_employeeService);
            var employee = await employeeOperations.SearchEmployeeAsync(id);
            return View(employee);
        }

        public async Task<IActionResult> DisplayAllEmployees()
        {
            var employeeOperations = new EmployeeOperations(_employeeService);
            var employees = await employeeOperations.DisplayAllEmployeesAsync();
            return View(employees);
        }
    }
}