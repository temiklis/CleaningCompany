using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Implementations
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
    }
}
