using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class OrderRequestRepository : Repository<OrderRequest>, IOrderRequestRepository
    {
        public OrderRequestRepository(ApplicationContext context) : base(context)
        {

        }

        public IQueryable<OrderRequest> GetOrderRequestsWithProducts()
        {
            return _context.OrderRequests
                .Include(or => or.Products)
                .AsQueryable();
        }

        public async Task<OrderRequest> GetOrderRequestWithProducts(int id)
        {
            return await _context.OrderRequests
                .Include(or => or.Products)
                .ThenInclude(prod => prod.Materials)
                .Where(or => or.Id == id)
                .FirstOrDefaultAsync();
        }

        public IQueryable<OrderRequest> GetUserOrderRequests(string email)
        {
            return _context.OrderRequests
                .Include(or => or.Products)
                .Where(or => or.Email.ToUpper() == email.ToUpper());
        }
    }
}
