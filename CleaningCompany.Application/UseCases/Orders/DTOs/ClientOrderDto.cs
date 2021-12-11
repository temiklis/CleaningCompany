using System;

namespace CleaningCompany.Application.UseCases.Orders.DTOs
{
    public class ClientOrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RenderedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string Products { get; set; }
        public string Status { get; set; }
    }
}
