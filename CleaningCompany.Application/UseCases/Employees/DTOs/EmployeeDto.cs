using System;

namespace CleaningCompany.Application.UseCases.Employees.DTOs
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public decimal SalaryPerHour { get; set; }
        public bool IsFired { get; set; }
    }
}
