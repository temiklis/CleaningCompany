using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.Commands;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Domain.Entities;
using System;

namespace CleaningCompany.Application.UseCases.Materials
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, MaterialDto>()
                .ForMember(m => m.Price, s => s.MapFrom(x => (decimal)Math.Round(x.Price, 2)));
            CreateMap<Material, MaterialWithProductsDto>();

            CreateMap<CreateMaterialCommand, Material>();
            CreateMap<UpdateMaterialCommand, Material>();
        }
    }
}
