using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            return await _context.Clients
                .Where(cl => cl.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
