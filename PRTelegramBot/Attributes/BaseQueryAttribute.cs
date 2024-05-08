using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class BaseQueryAttribute<T> : Attribute, IBotIdentifier, ICommandStore<T>
    {
        protected List<T> commands = new List<T>();
        /// <summary>
        /// Коллекция команд.
        /// </summary>
        public IEnumerable<T> Commands { get { return commands.ToList(); } }

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
