using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeOperations
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeOperations(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeService.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
        }

        public async Task<Employee> SearchEmployeeAsync(string id)
        {
            return await _employeeService.GetEmployeeAsync(id);
        }

        public async Task<IEnumerable<Employee>> DisplayAllEmployeesAsync()
        {
            return await _employeeService.GetAllEmployeesAsync();
        }
    }
}