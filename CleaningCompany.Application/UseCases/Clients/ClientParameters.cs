namespace CleaningCompany.Application.UseCases.Clients
{
    public class ClientParameters : QueryStringParameters
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
