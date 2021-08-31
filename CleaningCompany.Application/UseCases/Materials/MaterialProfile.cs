using AutoMapper;
using CleaningCompany.Application.UseCases.Materials.Commands;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.Materials
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, MaterialDto>();
            CreateMap<Material, MaterialWithProductsDto>();

            CreateMap<CreateMaterialCommand, Material>();
            CreateMap<UpdateMaterialCommand, Material>();
        }
    }
}
