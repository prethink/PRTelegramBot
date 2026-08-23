using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Command that carries the entity identifier.
    /// </summary>
    public class EntityTCommand<T> : TCommandBase
    {
        #region Fields and properties

        /// <summary>
        /// Entity identifier
        /// </summary>
        [JsonPropertyName("1")]
        public T EntityId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        public EntityTCommand(T entityId)
            : base(0)
        {
            EntityId = entityId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        /// <param name="lastCommand">Previous command.</param>
        public EntityTCommand(T entityId, int lastCommand)
            : base(lastCommand)
        {
            EntityId = entityId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        /// <param name="action">Action to perform on the previous message.</param>
        public EntityTCommand(T entityId, ActionWithLastMessage action)
            : base(action)
        {
            EntityId = entityId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        /// <param name="lastCommand">Previous command.</param>
        /// <param name="action">Action to perform on the previous message.</param>
        public EntityTCommand(T entityId, int lastCommand, ActionWithLastMessage action)
            : base(lastCommand, action)
        {
            EntityId = entityId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityTCommand() { }

        #endregion
    }
}
