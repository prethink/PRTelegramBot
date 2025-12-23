using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.BackgroundTasks
{
    /// <summary>
    /// Атрибут метаданных фоновой задачи.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PRBackgroundTaskAttribute : Attribute, IPRBackgroundTaskMetadata
    {
        #region IPRBackgroundTaskMetadata

        /// <inheritdoc />
        public HashSet<long> BotIds { get; } = new HashSet<long>();

        /// <inheritdoc />
        public Guid Id { get; private set; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public int? InitialDelaySeconds { get; private set; }

        /// <inheritdoc />
        public int? RepeatSeconds { get; private set; }

        /// <inheritdoc />
        public int? MaxErrorAttempts { get; private set; }

        /// <inheritdoc />
        public int? MaxRepeatCount { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название задачи.</param>
        public PRBackgroundTaskAttribute(string id, string name)
            : this(Array.Empty<long>(), id, name, null, null, null, null) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название задачи.</param>
        /// <param name="initialDelaySeconds">Задержка перед запуском задачи</param>
        public PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name)
            : this(Array.Empty<long>(), id, name, initialDelaySeconds, null, null, null) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название задачи.</param>
        /// <param name="repeatSeconds">Через сколько задача должна снова выполниться.</param>
        public PRBackgroundTaskAttribute(string id, string name, int repeatSeconds)
            : this(Array.Empty<long>(), id, name, null, null, repeatSeconds, null) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название задачи.</param>
        /// <param name="repeatSeconds">Через сколько задача должна снова выполниться.</param>
        /// <param name="maxRepeatCount">Через сколько задача должна снова выполниться.</param>
        public PRBackgroundTaskAttribute(string id, string name, int repeatSeconds, int maxRepeatCount)
            : this(Array.Empty<long>(), id, name, null, maxRepeatCount, repeatSeconds, null) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="initialDelaySeconds">Задержка перед запуском задачи</param>
        /// <param name="name">Название задачи.</param>
        /// <param name="repeatSeconds">Через сколько задача должна снова выполниться.</param>
        public PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name, int repeatSeconds)
            : this(Array.Empty<long>(), id, name, initialDelaySeconds, null, repeatSeconds, null) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="initialDelaySeconds">Задержка перед запуском задачи</param>
        /// <param name="name">Название задачи.</param>
        /// <param name="maxRepeatCount">Через сколько задача должна снова выполниться.</param>
        /// <param name="repeatSeconds">Через сколько задача должна снова выполниться.</param>
        public PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name, int repeatSeconds, int maxRepeatCount) 
            : this(Array.Empty<long>(), id, name, initialDelaySeconds, maxRepeatCount, repeatSeconds, null) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botsIds">Идентификатор ботов, для который будет использоваться задача.</param>
        /// <param name="id">Идентификатор.</param>
        /// <param name="name">Название задачи.</param>
        /// <param name="initialDelaySeconds">Задержка перед запуском задачи</param>
        /// <param name="maxRepeatCount">Через сколько задача должна снова выполниться. Значение -1 означает неограниченное количество попыток.</param>
        /// <param name="repeatSeconds">Через сколько задача должна снова выполниться.</param>
        /// <param name="maxErrorAttempts">Максимальное количество ошибок при котором дальнейшее выполнение задачи прекращается. Значение -1 означает неограниченное количество попыток.</param>
        public PRBackgroundTaskAttribute(long[] botsIds, string id, string name, int? initialDelaySeconds, int? maxRepeatCount, int? repeatSeconds, int? maxErrorAttempts)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Task id cannot be null or empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Task name cannot be null or empty.", nameof(name));

            BotIds.AddRange(botsIds);
            Id = Guid.Parse(id);
            Name = name;
            InitialDelaySeconds = initialDelaySeconds;
            RepeatSeconds = repeatSeconds;
            MaxErrorAttempts = maxErrorAttempts;
            MaxRepeatCount = maxRepeatCount;
        }

        #endregion
    }
}
