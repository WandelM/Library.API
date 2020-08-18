using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public bool HasNext => (CurrentPage < TotalPages);
        public bool HasPrevious => CurrentPage > 0;

        public PaginatedList(List<T> items, int pageSize, int totalCount, int pageNumber)
        {
            PageSize = pageSize;
            TotalCount = totalCount;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<T>> GetPaginatedList(IQueryable<T> source, int pageSize, int pageNumber)
        {
            var totalCount = source.Count();
            var paginatedItems = await source.Skip(pageSize * pageNumber).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(paginatedItems, pageSize, totalCount, pageNumber);
        }
    }
}
