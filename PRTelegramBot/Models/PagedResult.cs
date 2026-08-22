namespace PRTelegramBot.Models
{
    /// <summary>
    /// Class for paginated data output.
    /// </summary>
    public abstract class PagedResultBase
    {
        #region Fields and properties

        /// <summary>
        /// Number of the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Number of pages.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// The first row item on the page.
        /// </summary>
        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        /// <summary>
        /// The last row item on the page.
        /// </summary>
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }

        #endregion
    }

    /// <summary>
    /// Class for paginated data output.
    /// </summary>
    /// <typeparam name="T">Any class type.</typeparam>
    public class PagedResult<T> : PagedResultBase 
        where T : class
    {
        #region Fields and properties

        /// <summary>
        /// Result.
        /// </summary>
        public IList<T> Results { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PagedResult()
        {
            Results = new List<T>();
        }

        #endregion
    }
}
