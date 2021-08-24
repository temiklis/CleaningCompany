using CleaningCompany.Application.UseCases.Materials.DTOs;
using System.Collections.Generic;

namespace CleaningCompany.Application.UseCases.Products.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string Difficulty { get; set; }
        public int OrdersAmount { get; set; }

        public IEnumerable<MaterialDto> Materials { get; set; }

    }
}
