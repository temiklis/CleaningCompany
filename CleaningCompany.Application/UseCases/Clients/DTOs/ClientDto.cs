namespace CleaningCompany.Application.UseCases.Clients.DTOs
{
    public class ClientDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Discount { get; set; }
        public int OrdersAmount { get; set; }
    }
}
