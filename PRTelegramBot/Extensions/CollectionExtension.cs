namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Расширения для работы с двумерными коллекциями
    /// (<see cref="IEnumerable{IEnumerable{T}}"/>).
    /// Упрощают получение строк, столбцов и размеров коллекции.
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// Возвращает строку (row) по указанному индексу.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="source">Двумерная коллекция.</param>
        /// <param name="rowIndex">Индекс строки (начиная с 0).</param>
        /// <returns>
        /// Коллекция элементов строки или пустая коллекция,
        /// если индекс некорректен или строка отсутствует.
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
        /// Возвращает столбец (column) по указанному индексу.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="source">Двумерная коллекция.</param>
        /// <param name="columnIndex">Индекс столбца (начиная с 0).</param>
        /// <returns>
        /// Коллекция элементов столбца или пустая коллекция,
        /// если индекс некорректен или столбец отсутствует.
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
        /// Возвращает количество строк в двумерной коллекции.
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="source">Двумерная коллекция.</param>
        /// <returns>Количество строк.</returns>
        public static long GetRowCount<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Count();
        }

        /// <summary>
        /// Возвращает максимальное количество элементов в строках
        /// (фактическое количество столбцов).
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции.</typeparam>
        /// <param name="source">Двумерная коллекция.</param>
        /// <returns>Количество столбцов.</returns>
        public static long GetColumnCount<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Max(row => row.Count());
        }
    }
}
