using System;

namespace CleaningCompany.Application.UseCases.OrderRequests.DTOs
{
    public class OrderRequestDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserFIO { get; set; }
        public string UserAddress { get; set; }
        public string Products { get; set; }
        public DateTime RenderedDate { get; set; }
        public DateTime RequestedDate { get; set; }

    }
}
