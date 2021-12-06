using CleaningCompany.Domain.Entities;
using System.Linq;

namespace CleaningCompany.Application.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        IQueryable<Client> GetAllClientsWithOrders();
    }
}
