namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Типы команд.
    /// </summary>
    public enum CommandType
    {
        None = 0,
        Reply,
        ReplyDynamic,
        Slash,
        NextStep,
        Inline,
        Message,
        Custom
    }
}
