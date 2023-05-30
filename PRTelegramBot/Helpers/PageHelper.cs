namespace PRTelegramBot.Helpers
{
    /// <summary>
    /// Помогает разбить данные постранично
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// Вывод данных постранично
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Коллекция данных</param>
        /// <param name="page">Страница</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Страница данных с доп информацией</returns>
        public static async Task<PagedResult<T>> GetPaged<T>(this IList<T> query,
                                         int page, int pageSize) where T : class
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

        //Для EntityFramework 

        //public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
        //                                         int page, int pageSize) where T : class
        //{
        //    var result = new PagedResult<T>();
        //    result.CurrentPage = page;
        //    result.PageSize = pageSize;
        //    result.RowCount = query.Count();


        //    var pageCount = (double)result.RowCount / pageSize;
        //    result.PageCount = (int)Math.Ceiling(pageCount);

        //    var skip = (page - 1) * pageSize;
        //    result.Query = query.Skip(skip).Take(pageSize);
        //    result.Results = result.Query.ToList();

        //    return result;
        //}
    }

    /// <summary>
    /// Струтура класса для постраничного вывода
    /// </summary>
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    /// <summary>
    /// Класс для постраничного вывода с типом лист
    /// </summary>
    /// <typeparam name="T">Любой объект</typeparam>
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
