using CleaningCompany.Domain.Entities;
using System.Linq;

namespace CleaningCompany.Application.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAllOrders();
    }
}
