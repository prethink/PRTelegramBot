using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи данных о идентификаторе сущности
    /// </summary>
    public class EntityTCommand<T> : TCommandBase
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        [JsonPropertyName("1")]
        public T EntityId { get; set; }

        public EntityTCommand(T entityId, int command = 0) : base(command)
        {
            EntityId = entityId;
        }
    }
}
