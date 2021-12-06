using CleaningCompany.Domain.Entities;
using System.Linq;

namespace CleaningCompany.Application.Interfaces
{
    public interface IOrderRequestRepository : IRepository<OrderRequest>
    {
        IQueryable<OrderRequest> GetOrderRequestsWithProducts();
        IQueryable<OrderRequest> GetUserOrderRequests(string email);
    }
}
