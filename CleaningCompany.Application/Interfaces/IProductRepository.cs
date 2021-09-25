using CleaningCompany.Domain.Entities;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductWithMaterials(int id);
    }
}
