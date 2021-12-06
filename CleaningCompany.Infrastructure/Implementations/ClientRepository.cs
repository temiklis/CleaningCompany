using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext context) : base(context)
        {

        }

        public IQueryable<Client> GetAllClientsWithOrders()
        {
            return _context.Clients
                .Include(c => c.Orders)
                .AsQueryable();
        }
    }
}
