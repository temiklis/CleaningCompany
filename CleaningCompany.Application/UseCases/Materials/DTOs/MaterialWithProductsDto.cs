using CleaningCompany.Application.UseCases.Products.DTOs;
using System.Collections.Generic;

namespace CleaningCompany.Application.UseCases.Materials.DTOs
{
    public class MaterialWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<ProductDto> Products { get; set; }
    }
}
