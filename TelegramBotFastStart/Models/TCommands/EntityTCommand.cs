using Newtonsoft.Json;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи данных о идентификаторе сущности
    /// </summary>
    internal class EntityTCommand : TCommandBase
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        [JsonProperty("1")]
        public long EntityId { get; set; }

        public EntityTCommand(long entityId)
        {
            EntityId = entityId;
        }
    }
}
