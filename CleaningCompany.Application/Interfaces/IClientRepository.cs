using CleaningCompany.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        IQueryable<Client> GetAllClientsWithOrders();

        Task<Client> GetClientByEmailAsync(string email);
    }
}
