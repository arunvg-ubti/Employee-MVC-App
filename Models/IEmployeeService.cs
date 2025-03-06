namespace EmployeeManagementSystem.Models
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeAsync(string id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(string id);
    }
}