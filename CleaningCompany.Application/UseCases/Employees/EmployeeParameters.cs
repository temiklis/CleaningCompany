namespace CleaningCompany.Application.UseCases.Employees
{
    public class EmployeeParameters : QueryStringParameters
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
