using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Results
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int? PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(IEnumerable<T> items, int count, int pageNumber, int? pageSize)
        {
            TotalCount = count;
            PageSize = pageSize ?? TotalCount;
            CurrentPage = pageNumber;
            TotalPages = pageSize.HasValue ? (int)Math.Ceiling(count / (double)pageSize) : 1;
            AddRange(items);
        }

        public static async Task<PagedList<TResponse>> ToPagedList<TResponse>(
            IQueryable<T> source,
            int pageNumber,
            int? pageSize,
            Func<IEnumerable<T>, IEnumerable<TResponse>> mapperHandler)
        {
            var count = source.Count();
            List<T> items = new List<T>();

            if (!pageSize.HasValue)
            {
                items = source.ToList();
            }
            else
            {
                items = source.Skip((pageNumber - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .ToList();
            }

            var result = mapperHandler.Invoke(items);
            return new PagedList<TResponse>(result, count, pageNumber, pageSize);
        }

    }

    public static class PagedListExtension
    {
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int? pageSize)
        {
            var count = source.Count();

            List<T> items = new List<T>();
            if (!pageSize.HasValue)
            {
                items = source.ToList();
            }
            else
            {
                items = source.Skip((pageNumber - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .ToList();
            }

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

