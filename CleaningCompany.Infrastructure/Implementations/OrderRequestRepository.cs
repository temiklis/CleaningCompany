using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Infrastructure.Persistence;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class OrderRequestRepository : Repository<OrderRequest>, IOrderRequestRepository
    {
        public OrderRequestRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
