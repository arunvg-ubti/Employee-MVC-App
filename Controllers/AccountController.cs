using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private static readonly List<string> TokenDatabase = new List<string>(); // In-memory database for tokens

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.IsAdmin = TempData["Role"]?.ToString() == "Admin";

                try
                {
                    await _userService.RegisterUserAsync(user);
                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login");
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError("", "Database error: " + dbEx.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred: " + ex.Message);
                }
            }
            return View(user);
        }

        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View();
            }

            // Check if a token exists in session (for validation on login attempt)
            var existingToken = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(existingToken))
            {
                // Validate the existing token
                if (!ValidateToken(existingToken))
                {
                    ModelState.AddModelError("", "Invalid or expired token. Please log in again.");
                    HttpContext.Session.Clear(); // Clear invalid token
                    return View();
                }
                // Token is valid, proceed to dashboard
                var userRole = HttpContext.Session.GetString("UserRole");
                return RedirectToAction(userRole == "Admin" ? "AdminDashboard" : "UserDashboard", "Employee");
            }

            // No token exists, validate credentials and generate a new token
            var user = await _userService.LoginUserAsync(login.Username, login.Password);
            if (user != null)
            {
                var token = GenerateJwtToken(user);
                // Store token in in-memory database
                TokenDatabase.Add(token);
                Console.WriteLine($"DEBUG: Token added to in-memory database: {token}");

                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("UserRole", user.IsAdmin ? "Admin" : "User");
                Console.WriteLine($"DEBUG: Login - JWToken set in session: {token}");

                return RedirectToAction(user.IsAdmin ? "AdminDashboard" : "UserDashboard", "Employee");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public IActionResult Logout()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                TokenDatabase.Remove(token); // Remove token from in-memory database
                Console.WriteLine($"DEBUG: Token removed from in-memory database: {token}");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var keyValue = jwtSettings["Key"];

            Console.WriteLine($"DEBUG: JWT Key from config: '{keyValue}'");

            if (string.IsNullOrEmpty(keyValue))
            {
                throw new InvalidOperationException("JWT Key is missing or empty in appsettings.json.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool ValidateToken(string token)
        {
            // Check if token exists in in-memory database
            if (!TokenDatabase.Contains(token))
            {
                Console.WriteLine("DEBUG: Token not found in in-memory database");
                return false;
            }

            var jwtSettings = _configuration.GetSection("Jwt");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                Console.WriteLine("DEBUG: Token validated successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Token validation failed: {ex.Message}");
                return false;
            }
        }
    }
}