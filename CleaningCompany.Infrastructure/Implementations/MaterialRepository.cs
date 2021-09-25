using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<Material> GetMaterialWithProducts(int id)
        {
            return await _context.Materials
                .Include(m => m.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
