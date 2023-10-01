using Newtonsoft.Json;

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
        [JsonProperty("1")]
        public T EntityId { get; set; }

        public EntityTCommand(T entityId)
        {
            EntityId = entityId;
        }
    }
}
