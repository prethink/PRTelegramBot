using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using System.Reflection;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for background tasks.
    /// </summary>
    public static class BackgroundTaskExtension
    {
        /// <summary>
        /// Gets the task metadata from the task itself.
        /// </summary>
        /// <param name="backgroundTask">Task.</param>
        /// <param name="metadates">Collection of existing metadata.</param>
        /// <param name="throwIfNull">Throw an exception when the value is null.</param>
        /// <returns>The metadata, or null.</returns>
        public static IPRBackgroundTaskMetadata GetMetadata(this IPRBackgroundTask backgroundTask, IEnumerable<IPRBackgroundTaskMetadata> metadates, bool throwIfNull = true)
        {
            var metadata = backgroundTask.GetType().GetCustomAttribute(typeof(PRBackgroundTaskAttribute), false) as IPRBackgroundTaskMetadata;

            if (metadata == null)
                metadata = backgroundTask as IPRBackgroundTaskMetadata;

            if (metadata == null)
                metadata = metadates.SingleOrDefault(x => x.Id == backgroundTask.Id);

            if (throwIfNull && metadata == null)
                throw new InvalidOperationException(
                    $"No metadata found for background task '{backgroundTask.GetType().FullName}'. " +
                    $"Make sure the metadata is preloaded into {nameof(PRBackgroundTaskRunner)}, " +
                    $"or that the task implements {nameof(IPRBackgroundTaskMetadata)}, " +
                    $"or uses the {nameof(PRBackgroundTaskAttribute)} attribute.");

            return metadata;
        }

        /// <summary>
        /// Gets the task metadata from the task itself.
        /// </summary>
        /// <param name="backgroundTask">Task.</param>
        /// <param name="throwIfNull">Throw an exception when the value is null.</param>
        /// <returns>The metadata, or null.</returns>
        public static IPRBackgroundTaskMetadata GetMetadata(this IPRBackgroundTask backgroundTask, bool throwIfNull = true)
        {
            return GetMetadata(backgroundTask, Enumerable.Empty<IPRBackgroundTaskMetadata>(), throwIfNull);
        }

        /// <summary>
        /// Returns the repeat interval.
        /// Null or a value ≤ 0 means no repeats.
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
