namespace PRTelegramBot.Models
{
    /// <summary>
    /// Класс для постраничного вывода данных.
    /// </summary>
    public abstract class PagedResultBase
    {
        #region Поля и свойства

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Количество страниц.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Количество строк.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Первый элемент строки на странице.
        /// </summary>
        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        /// <summary>
        /// Последний элемент строки на странице.
        /// </summary>
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }

        #endregion
    }

    /// <summary>
    /// Класс для постраничного вывода данных.
    /// </summary>
    /// <typeparam name="T">Любой тип класса.</typeparam>
    public class PagedResult<T> : PagedResultBase 
        where T : class
    {
        #region Поля и свойства

        /// <summary>
        /// Результат.
        /// </summary>
        public IList<T> Results { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public PagedResult()
        {
            Results = new List<T>();
        }

        #endregion
    }
}
