using System;

namespace CleaningCompany.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMaterialRepository Materials { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
    }
}
