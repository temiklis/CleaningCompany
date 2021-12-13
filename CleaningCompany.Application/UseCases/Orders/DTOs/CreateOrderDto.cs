using System.Collections.Generic;

namespace CleaningCompany.Application.UseCases.Orders.DTOs
{
    public class CreateOrderDto
    {
        public int OrderRequestId { get; set; }
        public ICollection<string> Employees { get; set; }
    }
}
