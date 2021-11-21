using CleaningCompany.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductWithMaterials(int id);
        Task<List<Product>> GetProductsByIds(List<int> ids);
    }
}
