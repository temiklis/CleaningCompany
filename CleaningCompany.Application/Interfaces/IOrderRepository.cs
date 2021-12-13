using CleaningCompany.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAllOrders();

        IQueryable<Order> GetClientOrders(string clientId);

        IQueryable<Order> GetEmployeeAssignedOrders(string employeeId);

        Task<Order> GetOrderWithClient(int id);
    }
}
