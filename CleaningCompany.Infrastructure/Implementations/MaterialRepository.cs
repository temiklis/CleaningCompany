using CleaningCompany.Domain.Entities;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Material>> GetMaterialsForProduct(int id)
        {
            var materials = await _context.Products
                .Include(p => p.Materials)
                .Where(p => p.Id == id)
                .Select(p => p.Materials)
                .FirstOrDefaultAsync();

            return materials.AsEnumerable();
        }

        public async Task<Material> GetMaterialWithProducts(int id)
        {
            return await _context.Materials
                .Include(m => m.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
        }


    }
}
