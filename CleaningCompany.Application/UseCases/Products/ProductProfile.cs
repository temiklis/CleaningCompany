using AutoMapper;
using CleaningCompany.Application.UseCases.Products.Commands;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
