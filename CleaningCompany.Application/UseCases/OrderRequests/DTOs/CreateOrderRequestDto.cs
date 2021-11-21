using CleaningCompany.Application.UseCases.Products.DTOs;
using System;
using System.Collections.Generic;

namespace CleaningCompany.Application.UseCases.OrderRequests.DTOs
{
    public class CreateOrderRequestDto
    {
        public string Email { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }

        public ICollection<ProductCardDto> Products { get; set; }
    }
}
