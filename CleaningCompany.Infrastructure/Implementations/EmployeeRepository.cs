using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Employee>> GetIdleEmployeesByOrderDate(DateTime date)
        {
            var employees = _context.Employees
                .Include(emp => emp.AssignedOrders)
                .Where(emp =>  !emp.AssignedOrders.Any()
                || emp.AssignedOrders.Any(o => o.RenderedDate.Date >= DateTime.Now.Date && o.RenderedDate.Date != date.Date));
            
            return await employees.ToListAsync();
        }
    }
}
