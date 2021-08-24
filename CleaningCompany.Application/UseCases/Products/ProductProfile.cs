using AutoMapper;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
