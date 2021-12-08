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
                .Where(o => o.ResponsibleEmployees.Any(r => r.Id == employeeId));

            //return _context.Employees
            //    .Include(e => e.AssignedOrders)
            //    .ThenInclude(o => o.Products)
            //    .Include(e => e.AssignedOrders)
            //    .ThenInclude(o => o.ResponsibleEmployees)
            //    .Where(e => e.Id == employeeId)
            //    .SelectMany(e => e.AssignedOrders);
        }

        public IQueryable<Order> GetClientOrders(string clientId)
        {
            return _context.Clients
                .Include(c => c.Orders)
                .ThenInclude(o => o.Products)
                .Where(c => c.Id == clientId)
                .SelectMany(c => c.Orders);
        }
    }
}
