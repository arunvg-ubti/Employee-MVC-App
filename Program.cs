using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models; // ✅ Ensure models are included
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
 
var builder = WebApplication.CreateBuilder(args);
 
// ✅ Load configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true) // ✅ Ensures the config file is loaded
    .Build();
 
// ✅ Retrieve connection string correctly
var connectionString = configuration.GetConnectionString("DefaultConnection");
 
// ✅ Debug log to check if connection string is being loaded
Console.WriteLine($"🔍 DEBUG: Connection String from appsettings.json → {connectionString}");
 
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("❌ ERROR: Connection string is NULL or EMPTY.");
}
 
// ✅ Register DbContext with EF Core and SQL Server
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(connectionString));
 
// ✅ Register services for Dependency Injection (Prevents Service Resolution Errors)
builder.Services.AddScoped<IUserService, UserService>();   // Registers IUserService with UserService
builder.Services.AddScoped<IEmployeeService, EmployeeService>(); // Registers IEmployeeService with EmployeeService
 
// ✅ If you have repositories, register them too
// builder.Services.AddScoped<IUserRepository, UserRepository>();
 
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // ✅ Enable session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor(); // ✅ Ensures controllers can access session data
 
var app = builder.Build();
 
// ✅ Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
 
// ✅ Enable session before routing
app.UseSession();
 
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
 
// ✅ Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
 
// ✅ Run the application asynchronously
await app.RunAsync();
 