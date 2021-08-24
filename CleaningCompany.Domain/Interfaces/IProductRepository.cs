using CleaningCompany.Domain.Entities;
using System.Threading.Tasks;

namespace CleaningCompany.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductWithMaterials(int id);
    }
}
