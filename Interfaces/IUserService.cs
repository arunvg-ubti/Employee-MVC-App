namespace EmployeeManagementSystem.Models
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<User> LoginUserAsync(string username, string password);
    }
}