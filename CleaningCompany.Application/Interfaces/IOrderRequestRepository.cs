using CleaningCompany.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IOrderRequestRepository : IRepository<OrderRequest>
    {
        IQueryable<OrderRequest> GetOrderRequestsWithProducts();
        Task<OrderRequest> GetOrderRequestWithProductsAndMaterials(int id);
        Task<OrderRequest> GetOrderRequestWithProducts(int id);
        IQueryable<OrderRequest> GetUserOrderRequests(string email);
    }
}
