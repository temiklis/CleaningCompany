using CleaningCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleaningCompany.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRequest> OrderRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
