using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.BackgroundTasks
{
    /// <summary>
    /// Background task metadata attribute.
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

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Task name.</param>
        public PRBackgroundTaskAttribute(string id, string name)
            : this(Array.Empty<long>(), id, name, null, null, null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Task name.</param>
        /// <param name="initialDelaySeconds">Delay before the task starts</param>
        public PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name)
            : this(Array.Empty<long>(), id, name, initialDelaySeconds, null, null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Task name.</param>
        /// <param name="repeatSeconds">How long until the task should run again.</param>
        public PRBackgroundTaskAttribute(string id, string name, int repeatSeconds)
            : this(Array.Empty<long>(), id, name, null, null, repeatSeconds, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Task name.</param>
        /// <param name="repeatSeconds">How long until the task should run again.</param>
        /// <param name="maxRepeatCount">How long until the task should run again.</param>
        public PRBackgroundTaskAttribute(string id, string name, int repeatSeconds, int maxRepeatCount)
            : this(Array.Empty<long>(), id, name, null, maxRepeatCount, repeatSeconds, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="initialDelaySeconds">Delay before the task starts</param>
        /// <param name="name">Task name.</param>
        /// <param name="repeatSeconds">How long until the task should run again.</param>
        public PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name, int repeatSeconds)
            : this(Array.Empty<long>(), id, name, initialDelaySeconds, null, repeatSeconds, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="initialDelaySeconds">Delay before the task starts</param>
        /// <param name="name">Task name.</param>
        /// <param name="maxRepeatCount">How long until the task should run again.</param>
        /// <param name="repeatSeconds">How long until the task should run again.</param>
        public PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name, int repeatSeconds, int maxRepeatCount) 
            : this(Array.Empty<long>(), id, name, initialDelaySeconds, maxRepeatCount, repeatSeconds, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botsIds">Identifiers of the bots the task will be used for.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Task name.</param>
        /// <param name="initialDelaySeconds">Delay before the task starts</param>
        /// <param name="maxRepeatCount">How long until the task should run again. A value of -1 means an unlimited number of attempts.</param>
        /// <param name="repeatSeconds">How long until the task should run again.</param>
        /// <param name="maxErrorAttempts">Maximum number of errors after which the task stops running. A value of -1 means an unlimited number of attempts.</param>
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
