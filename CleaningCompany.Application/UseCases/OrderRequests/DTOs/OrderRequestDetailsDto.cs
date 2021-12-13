using System;
using System.Collections.Generic;

namespace CleaningCompany.Application.UseCases.OrderRequests.DTOs
{
    public class OrderRequestDetailsDto
    {
        public int Id { get; set; }
        public string UserFIO { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public List<string> Products { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime RenderedDate { get; set; }

    }
}
