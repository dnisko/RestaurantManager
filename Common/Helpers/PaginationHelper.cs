using Common.Responses;
using DTOs.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Common.Helpers
{
    public class PaginationHelper
    {
        public static async Task<PaginatedResult<T>> ApplyPaginationAsync<T>(
            IQueryable<T> query,
            IPaginationParams baseParams)
        {
            var entityType = typeof(T);

            // Universal filtering by Id
            if (baseParams.Id.HasValue && entityType.GetProperty("Id") != null)
            {
                query = query.Where(x => EF.Property<int>(x, "Id") == baseParams.Id.Value);
            }

            // Universal search by Name
            if (!string.IsNullOrWhiteSpace(baseParams.SearchKeyword) && entityType.GetProperty("Name") != null)
            {
                query = query.Where(x =>
                    EF.Functions.Like(EF.Property<string>(x, "Name"), $"%{baseParams.SearchKeyword}%"));
            }
            // Category-specific filters

            // Recipe-specific filters


            // Sorting
            if (!string.IsNullOrWhiteSpace(baseParams.SortBy))
            {
                var sortProperty = entityType.GetProperty(baseParams.SortBy,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (sortProperty != null)
                {
                    query = baseParams.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(x => EF.Property<object>(x, sortProperty.Name))
                        : query.OrderBy(x => EF.Property<object>(x, sortProperty.Name));
                }
            }
            else
            {
                // Apply default sort by Id
                var defaultSort = entityType.GetProperty("Id"); // or "Username" or any field you like
                if (defaultSort != null)
                {
                    query = query.OrderBy(x => EF.Property<object>(x, defaultSort.Name));
                }
            }

            // Count and paginate
            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((baseParams.PageNumber - 1) * baseParams.PageSize)
                .Take(baseParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<T>(items, totalRecords, baseParams.PageNumber, baseParams.PageSize);

        }
    }
}
