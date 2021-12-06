using CleaningCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetSingleAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
