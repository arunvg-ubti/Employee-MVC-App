using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using EmployeeManagementSystem.Models;
using System;
 
namespace EmployeeManagementSystem.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Load configuration from Config/appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
 
                var connectionString = configuration.GetSection("Config:ConnectionStrings:DefaultConnection").Value;
 
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    throw new InvalidOperationException("The ConnectionString property has not been initialized.");
                }
            }
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensuring Username is the primary key for Users table
            modelBuilder.Entity<User>()
                .HasKey(u => u.Username);
        }
    }
}
 