using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Implementations
{
    internal class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
