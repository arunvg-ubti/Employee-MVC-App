namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public DateTime DateOfJoining { get; set; }
    }

    public enum Position
    {
        Manager,
        Developer,
        Sales_Executive,
        HR_Specialist,
        Marketing_Manager
    }
}