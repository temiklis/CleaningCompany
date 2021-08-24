using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class OrderRepository: Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
