using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Infrastructure.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {

        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Client)
                .Include(o => o.ResponsibleEmployees)
                .Include(o => o.Products)
                .AsQueryable();
        }

        public IQueryable<Order> GetEmployeeAssignedOrders(string employeeId)
        {
            return _context.Orders
                .Include(o => o.Products)
                .Include(o => o.ResponsibleEmployees)
                .Include(o => o.OrderRequest)
                .Where(o => o.ResponsibleEmployees.Any(r => r.Id == employeeId));
        }

        public IQueryable<Order> GetClientOrders(string clientId)
        {
            return _context.Orders
                .Include(o => o.Products)
                .Include(o => o.Client)
                .Where(o => o.ClientId == clientId);
        }
    }
}
