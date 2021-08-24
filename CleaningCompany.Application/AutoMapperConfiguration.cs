using AutoMapper;
using CleaningCompany.Application.UseCases.Materials;
using CleaningCompany.Application.UseCases.Orders;
using CleaningCompany.Application.UseCases.Products;

namespace CleaningCompany.Application
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetMapperConfiguration()
        {
            var config = new MapperConfiguration(x =>
              {
                  x.AddProfile(new MaterialProfile());
                  x.AddProfile(new ProductProfile());
                  x.AddProfile(new OrderProfile());
              });

            return config.CreateMapper();
        }
    }
}
