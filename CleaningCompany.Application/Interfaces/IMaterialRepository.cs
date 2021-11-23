using CleaningCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<Material> GetMaterialWithProducts(int id);
        Task<IEnumerable<Material>> GetMaterialsForProduct(int id);
        IQueryable<Material> GetMaterialsWithProducts();
    }
}
