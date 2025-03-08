namespace EmployeeManagementSystem.Models
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdOrNameAsync(string searchQuery);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(string id);
    }
}