namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Command types.
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
