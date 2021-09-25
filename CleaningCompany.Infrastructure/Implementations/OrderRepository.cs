using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Infrastructure.Persistence;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class OrderRepository: Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
