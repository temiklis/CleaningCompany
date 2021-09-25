﻿using AutoMapper;
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

            CreateMap<Product, ProductDto>();
        }
    }
}
