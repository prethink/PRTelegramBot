using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using System.Reflection;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для фоновых задач.
    /// </summary>
    public static class BackgroundTaskExtension
    {
        /// <summary>
        /// Получить метаданные задачи из задачи.
        /// </summary>
        /// <param name="backgroundTask">Задача.</param>
        /// <param name="metadates">Коллекция существующих метаданных.</param>
        /// <param name="throwIfNull">Выбросить exception если null.</param>
        /// <returns>Метаданные или null.</returns>
        public static IPRBackgroundTaskMetadata GetMetadata(this IPRBackgroundTask backgroundTask, IEnumerable<IPRBackgroundTaskMetadata> metadates, bool throwIfNull = true)
        {
            var metadata = backgroundTask.GetType().GetCustomAttribute(typeof(PRBackgroundTaskAttribute), false) as IPRBackgroundTaskMetadata;

            if (metadata == null)
                metadata = backgroundTask as IPRBackgroundTaskMetadata;

            if (metadata == null)
                metadata = metadates.SingleOrDefault(x => x.Id == backgroundTask.Id);

            if (throwIfNull && metadata == null)
                throw new InvalidOperationException(
                    $"Не найдены метаданные для фоновой задачи '{backgroundTask.GetType().FullName}'. " +
                    $"Убедитесь, что метаданные предварительно загружены в {nameof(PRBackgroundTaskRunner)}, " +
                    $"или задача реализует {nameof(IPRBackgroundTaskMetadata)}, " +
                    $"или использует атрибут {nameof(PRBackgroundTaskAttribute)}.");

            return metadata;
        }

        /// <summary>
        /// Получить метаданные задачи из задачи.
        /// </summary>
        /// <param name="backgroundTask">Задача.</param>
        /// <param name="throwIfNull">Выбросить exception если null.</param>
        /// <returns>Метаданные или null.</returns>
        public static IPRBackgroundTaskMetadata GetMetadata(this IPRBackgroundTask backgroundTask, bool throwIfNull = true)
        {
            return GetMetadata(backgroundTask, Enumerable.Empty<IPRBackgroundTaskMetadata>(), throwIfNull);
        }

        /// <summary>
        /// Возвращает интервал повторного выполнения.
        /// Null или значение ≤ 0 означает отсутствие повторов.
        /// </summary>
        public static int GetRepeatSeconds(this IPRBackgroundTaskMetadata metadata)
        {
            var value = metadata.RepeatSeconds.GetValueOrDefault();
            return value > 0 
                ? value 
                : 1;
        }
    }
}
