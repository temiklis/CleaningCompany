using System;

namespace CleaningCompany.Application.UseCases.OrderRequests.DTOs
{
    public class UserOrderRequestDto
    {
        public string Address { get; set; }
        public string Products { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime RenderedDate { get; set; }
    }
}
