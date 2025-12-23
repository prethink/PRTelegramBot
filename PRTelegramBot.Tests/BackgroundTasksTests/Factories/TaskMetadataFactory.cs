using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using PRTelegramBot.Tests.BackgroundTasksTests.Models;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Factories
{
    /// <summary>
    /// Фабрика тестовых метаданных фоновых задач.
    /// Используется для упрощения создания различных сценариев в тестах.
    /// </summary>
    public static class TaskMetadataFactory
    {
        /// <summary>
        /// Базовые валидные метаданные с дефолтными значениями.
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
        /// Метаданные для задачи с бесконечными повторами.
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
        /// Метаданные для одноразовой задачи без повторов.
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
        /// Метаданные для задачи с ограниченным количеством запусков.
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
        /// Метаданные для задачи с ограниченным количеством ошибок.
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
        /// Метаданные для задачи, предназначенной только для конкретного бота.
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
        /// Метаданные с кастомными параметрами (универсальный вариант).
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
        /// Метаданные без указанных параметров выполнения.
        /// Все опциональные значения равны null.
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
        /// Одноразовая задача (RepeatSeconds = null).
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
        /// Задача без начальной задержки (InitialDelaySeconds = null).
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
        /// Задача с неограниченным количеством запусков (MaxRepeatCount = null).
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
        /// Задача без ограничения на количество ошибок (MaxErrorAttempts = null).
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
        /// Смешанный сценарий с частично заданными параметрами.
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
