using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EmployeeManagementSystem.Data;

namespace EmployeeManagementSystem.Models
{
    public class UserService : IUserService
    {
        private readonly EmployeeContext _context;

        public UserService(EmployeeContext context)
        {
            _context = context;
        }

        public async Task RegisterUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception
                Console.WriteLine($"An error occurred while updating the database: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<User> LoginUserAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}