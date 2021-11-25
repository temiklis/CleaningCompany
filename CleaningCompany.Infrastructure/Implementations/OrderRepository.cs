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
            return this._context.Orders
                .Include(o => o.Client)
                .Include(o => o.ResponsibleEmployees)
                .Include(o => o.Products)
                .AsQueryable();
        }
    }
}
