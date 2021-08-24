﻿using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.Materials
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<CreateMaterialDto, Material>();
            CreateMap<Material, MaterialDto>();
        }
    }
}
