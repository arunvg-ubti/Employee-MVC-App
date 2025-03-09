using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Id { get; set; }
 
        [Required]
        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
 
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Position { get; set; }  // Now stored as string
 
        [Required]
        public int Salary { get; set; } 
 
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
 
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }
 
        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }
 
        [Required]
        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Department { get; set; }
 
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }
    }
}
 