namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extensions for working with two-dimensional collections
    /// (<see cref="IEnumerable{IEnumerable{T}}"/>).
    /// They make getting the rows, columns and sizes of a collection simpler.
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// Returns the row at the specified index.
        /// </summary>
        /// <typeparam name="T">Type of the collection elements.</typeparam>
        /// <param name="source">Two-dimensional collection.</param>
        /// <param name="rowIndex">Row index (zero-based).</param>
        /// <returns>
        /// The collection of items in the row, or an empty collection
        /// if the index is invalid or the row does not exist.
        /// </returns>
        public static IEnumerable<T> GetRow<T>(this IEnumerable<IEnumerable<T>> source, int rowIndex)
        {
            if (rowIndex < 0)
                return Enumerable.Empty<T>();

            return source
                .Skip(rowIndex)
                .Take(1)
                .FirstOrDefault()
                ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Adds the items to a <see cref="HashSet{T}"/>.
        /// Returns the number of items that were actually added.
        /// </summary>
        public static int AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var added = 0;

            foreach (var item in items)
            {
                if (set.Add(item))
                    added++;
            }

            return added;
        }

        /// <summary>
        /// Returns the column at the specified index.
        /// </summary>
        /// <typeparam name="T">Type of the collection elements.</typeparam>
        /// <param name="source">Two-dimensional collection.</param>
        /// <param name="columnIndex">Column index (zero-based).</param>
        /// <returns>
        /// The collection of items in the column, or an empty collection
        /// if the index is invalid or the column does not exist.
        /// </returns>
        public static IEnumerable<T> GetColumn<T>(this IEnumerable<IEnumerable<T>> source, int columnIndex)
        {
            if (columnIndex < 0)
                return Enumerable.Empty<T>();

            return source
                .Select(row =>
                {
                    if (row == null)
                        return default;

                    return row
                        .Skip(columnIndex)
                        .Take(1)
                        .FirstOrDefault();
                })
                .Where(x => x != null);
        }

        /// <summary>
        /// Returns the number of rows in the two-dimensional collection.
        /// </summary>
        /// <typeparam name="T">Type of the collection elements.</typeparam>
        /// <param name="source">Two-dimensional collection.</param>
        /// <returns>Number of rows.</returns>
        public static long GetRowCount<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Count();
        }

        /// <summary>
        /// Returns the maximum number of items across the rows
        /// (the actual number of columns).
        /// </summary>
        /// <typeparam name="T">Type of the collection elements.</typeparam>
        /// <param name="source">Two-dimensional collection.</param>
        /// <returns>Number of columns.</returns>
        public static long GetColumnCount<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Max(row => row.Count());
        }
    }
}
