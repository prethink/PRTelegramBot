namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для inline команд
    /// </summary>
    public class InlineCallbackHandlerAttribute<T> : BaseQueryAttribute<Enum> where T : Enum
    {
        public InlineCallbackHandlerAttribute(params T[] commands) 
            : this (0, commands) { }

        public InlineCallbackHandlerAttribute(long botId, params T[] commands) : base(botId)
        {
            foreach (var command in commands)
                Commands.Add((Enum)command);
        }
    }
}
