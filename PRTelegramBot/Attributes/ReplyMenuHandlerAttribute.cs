namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для reply методов
    /// </summary>
    public class ReplyMenuHandlerAttribute : BaseQueryAttribute<string>
    {
        public ReplyMenuHandlerAttribute(params string[] commands) 
            : this(0, commands) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="botId"></param>
        /// <param name="commands"></param>
        public ReplyMenuHandlerAttribute(long botId, params string[] commands) : base(botId)
        {
            this.commands.AddRange(commands);
        }
    }
}
