using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<Product> GetProductWithMaterials(int id)
        {
            return await _context.Products
                .Include(p => p.Materials)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
