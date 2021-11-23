namespace CleaningCompany.Application.UseCases.OrderRequests
{
    public class OrderRequestParameters : QueryStringParameters
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string FIO { get; set; }
    }
}
