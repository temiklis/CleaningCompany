﻿using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Interfaces;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
