using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи данных о идентификаторе сущности.
    /// </summary>
    public class EntityTCommand<T> : TCommandBase
    {
        #region Поля и свойства

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        [JsonPropertyName("1")]
        public T EntityId { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityId">Идентификатор сущности.</param>
        public EntityTCommand(T entityId)
            : base(0)
        {
            EntityId = entityId;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityId">Идентификатор сущности.</param>
        /// <param name="lastCommand">Прошлая команда.</param>
        public EntityTCommand(T entityId, int lastCommand)
            : base(lastCommand)
        {
            EntityId = entityId;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public EntityTCommand() { }

        #endregion
    }
}
