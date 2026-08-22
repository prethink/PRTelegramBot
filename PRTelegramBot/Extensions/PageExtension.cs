using PRTelegramBot.Models;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Helps split the data into pages.
    /// </summary>
    public static class PageExtension
    {
        #region Methods

        /// <summary>
        /// Outputs the data page by page.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="query">Collection of data.</param>
        /// <param name="page">Page.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>A page of data together with additional information.</returns>
        public static Task<PagedResult<T>> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize)
            where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return Task.FromResult(result);
        }

        #endregion
    }
}
