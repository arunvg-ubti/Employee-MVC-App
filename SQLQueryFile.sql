	--Creating Database 'EmployeesAssessment'
create database EmployeesAssessment;
go

--Using this db
use EmployeesAssessment;
go

--Creating table Employees

--Added more fields in 'Employees' table
CREATE TABLE Employees (
    Id NVARCHAR(20) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Position NVARCHAR(50) NOT NULL,  -- Enum stored as NVARCHAR
    Salary DECIMAL(20,2) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Department NVARCHAR(255) NOT NULL,
    DateOfJoining DATE NOT NULL
);
GO

-- Creating table Users
create table Users(Username nvarchar(50) primary key,
Password nvarchar(255), IsAdmin bit);
go

INSERT INTO Employees (Id, Name, Position, Salary, DateOfBirth, Email, PhoneNumber, Department, DateOfJoining) VALUES
('E101', 'John Doe', 'Manager', 75000, '1985-07-23', 'john.doe@example.com', '+1-234-567-8901', 'Human Resources', '2020-01-15'),
('E102', 'Jane Smith', 'Developer', 65000, '1990-05-12', 'jane.smith@example.com', '+1-234-567-8902', 'IT', '2019-03-10'),
('E103', 'Alice Johnson', 'Sales Executive', 55000, '1988-11-30', 'alice.johnson@example.com', '+1-234-567-8903', 'Sales', '2021-06-25'),
('E104', 'Bob Brown', 'HR Specialist', 60000, '1992-02-18', 'bob.brown@example.com', '+1-234-567-8904', 'Human Resources', '2018-09-05'),
('E105', 'Carol White', 'Marketing Manager', 70000, '1987-08-14', 'carol.white@example.com', '+1-234-567-8905', 'Marketing', '2022-01-20');
go

select * from users;

select * from employees;