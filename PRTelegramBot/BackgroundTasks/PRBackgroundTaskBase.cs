using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;

namespace PRTelegramBot.BackgroundTasks
{
    /// <summary>
    /// Базовый класс фоновых задач.
    /// </summary>
    public abstract class PRBackgroundTaskBase : IPRBackgroundTask, IPRBackgroundTaskMetadata
    {
        #region Поля и свойства

        /// <summary>
        /// Экземпляр бота.
        /// </summary>
        protected PRBotBase bot;

        #endregion

        #region IPRBackgroundTaskMetadata

        /// <inheritdoc/>
        public abstract Guid Id { get; }

        /// <inheritdoc/>
        public HashSet<long> BotIds { get; } = new HashSet<long>();

        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract int? InitialDelaySeconds { get; }

        /// <inheritdoc/>
        public abstract int? RepeatSeconds { get; }

        /// <inheritdoc/>
        public abstract int? MaxErrorAttempts { get; }

        /// <inheritdoc/>
        public abstract int? MaxRepeatCount { get; }

        #endregion

        #region IPRBackgroundTask

        /// <inheritdoc />
        public abstract Task ExecuteAsync(CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract Task<bool> CanExecute();

        /// <inheritdoc />
        public virtual Task Initialize(PRBotBase bot)
        {
            this.bot = bot;
            return Task.CompletedTask;
        }

        #endregion
    }
}
