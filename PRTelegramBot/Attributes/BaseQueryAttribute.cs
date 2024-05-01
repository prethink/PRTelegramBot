using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class BaseQueryAttribute<T> : Attribute, IBotIdentifier
    {
        /// <summary>
        /// Коллекция команд.
        /// </summary>
        public List<T> Commands { get; private set; } = new List<T>();

        #region IBotIdentifier

        /// <summary>
        /// Идентификатор бота
        /// </summary>
        public long BotId { get; set; }

        #endregion

        public BaseQueryAttribute(long botId = 0)
        {
            BotId = botId;
        }
    }
}
