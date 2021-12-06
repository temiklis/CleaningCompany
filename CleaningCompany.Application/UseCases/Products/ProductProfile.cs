using AutoMapper;
using CleaningCompany.Application.UseCases.Products.Commands;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Entities.Enums;
using System;

namespace CleaningCompany.Application.UseCases.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(d => d.Difficulty, opt => opt.MapFrom(x => Enum.Parse(typeof(Difficulty), x.Difficulty)));

            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Difficulty, opt => opt.MapFrom(x => ((Difficulty)x.Difficulty).ToString()))
                .ForMember(m => m.BasePrice, s => s.MapFrom(x => (decimal)Math.Round(x.BasePrice, 2)))
                .ForMember(p => p.OrdersAmount, s => s.MapFrom(p => p.Orders.Count));

            CreateMap<Product, ProductCardDto>()
                .ForMember(d => d.Difficulty, opt => opt.MapFrom(x => ((Difficulty)x.Difficulty).ToString()))
                .ForMember(d => d.DifficultyId, opt => opt.MapFrom(x => x.Difficulty))
                .ForMember(m => m.BasePrice, s => s.MapFrom(x => (decimal)Math.Round(x.BasePrice, 2))); ;
        }
    }
}
