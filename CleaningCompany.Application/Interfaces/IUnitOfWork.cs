using System;

namespace CleaningCompany.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMaterialRepository Materials { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderRequestRepository OrderRequests { get; }
        IEmployeeRepository Employees { get; }
        IClientRepository Clients { get; }
    }
}
