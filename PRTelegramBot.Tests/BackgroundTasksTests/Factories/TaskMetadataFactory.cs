using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using PRTelegramBot.Tests.BackgroundTasksTests.Models;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Factories
{
    /// <summary>
    /// Factory for background task metadata used in tests.
    /// Used to make building the various test scenarios simpler.
    /// </summary>
    public static class TaskMetadataFactory
    {
        /// <summary>
        /// Valid baseline metadata with default values.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateDefault(Guid? id = null, string? name = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = name ?? "TestBackgroundTask",
            };
        }

        /// <summary>
        /// Metadata for a task that repeats indefinitely.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateInfiniteRepeat(Guid? id = null, string? name = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = name ?? "InfiniteRepeatTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                RepeatSeconds = 1,
                MaxRepeatCount = -1,
            };
        }

        /// <summary>
        /// Metadata for a one-shot task with no repeats.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateOneTime(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "OneTimeTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                MaxRepeatCount = 0,
            };
        }

        /// <summary>
        /// Metadata for a task with a limited number of runs.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateWithLimitedRepeats(int? repeatSeconds, int maxRepeatCount, Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "LimitedRepeatTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                RepeatSeconds = repeatSeconds,
                MaxRepeatCount = maxRepeatCount,
                MaxErrorAttempts = 1
            };
        }

        /// <summary>
        /// Metadata for a task with a limited number of errors.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateWithErrorLimit(
            int? maxRepeatCount,
            int maxErrorAttempts,
            Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "ErrorLimitedTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                MaxRepeatCount = maxRepeatCount,
                MaxErrorAttempts = maxErrorAttempts
            };
        }

        /// <summary>
        /// Metadata for a task intended for one specific bot only.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateForBot(long botId, Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = $"BotSpecificTask_{botId}",
                BotIds = new HashSet<long> { botId },
                RepeatSeconds = 1,
                MaxRepeatCount = 1,
                MaxErrorAttempts = 1
            };
        }

        /// <summary>
        /// Metadata with custom parameters (the general-purpose variant).
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateCustom(
            Guid? id = null,
            string? name = null,
            IEnumerable<long>? botIds = null,
            int? initialDelaySeconds = null,
            int? repeatSeconds = null,
            int? maxRepeatCount = null,
            int? maxErrorAttempts = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = name ?? "CustomTask",
                BotIds = botIds != null
                    ? new HashSet<long>(botIds)
                    : new HashSet<long> { PRConstants.ALL_BOTS_ID },
                InitialDelaySeconds = initialDelaySeconds,
                RepeatSeconds = repeatSeconds,
                MaxRepeatCount = maxRepeatCount,
                MaxErrorAttempts = maxErrorAttempts
            };
        }

        /// <summary>
        /// Metadata with no execution parameters specified.
        /// All optional values are null.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateWithAllNulls(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "NullOptionsTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                InitialDelaySeconds = null,
                RepeatSeconds = null,
                MaxRepeatCount = null,
                MaxErrorAttempts = null
            };
        }

        /// <summary>
        /// A one-shot task (RepeatSeconds = null).
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateOneTimeWithNullRepeat(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "OneTimeNullRepeatTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                InitialDelaySeconds = 0,
                RepeatSeconds = null,
                MaxRepeatCount = 1,
                MaxErrorAttempts = 1
            };
        }

        /// <summary>
        /// A task with no initial delay (InitialDelaySeconds = null).
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateWithoutInitialDelay(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "NoInitialDelayTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                InitialDelaySeconds = null,
                RepeatSeconds = 1,
                MaxRepeatCount = 1,
                MaxErrorAttempts = 1
            };
        }

        /// <summary>
        /// A task with an unlimited number of runs (MaxRepeatCount = null).
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateInfiniteByNullRepeatCount(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "InfiniteByNullRepeatCountTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                RepeatSeconds = 1,
                MaxRepeatCount = null,
                MaxErrorAttempts = 1
            };
        }

        /// <summary>
        /// A task with no limit on the number of errors (MaxErrorAttempts = null).
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateWithoutErrorLimit(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "NoErrorLimitTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                RepeatSeconds = 1,
                MaxRepeatCount = 3,
                MaxErrorAttempts = null
            };
        }

        /// <summary>
        /// A mixed scenario with only some parameters specified.
        /// </summary>
        public static IPRBackgroundTaskMetadata CreateMixedNulls(Guid? id = null)
        {
            return new TestBackgroundTaskMetadata
            {
                Id = id ?? Guid.NewGuid(),
                Name = "MixedNullsTask",
                BotIds = new HashSet<long> { PRConstants.ALL_BOTS_ID },
                InitialDelaySeconds = null,
                RepeatSeconds = 2,
                MaxRepeatCount = null,
                MaxErrorAttempts = 2
            };
        }
    }
}
