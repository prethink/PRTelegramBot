using PRTelegramBot.Models;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Помогает разбить данные постранично
    /// </summary>
    public static class PageExtension
    {
        /// <summary>
        /// Вывод данных постранично.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Коллекция данных</param>
        /// <param name="page">Страница</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Страница данных с доп информацией</returns>
        public static async Task<PagedResult<T>> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize) 
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

            return result;
        }
    }
}
