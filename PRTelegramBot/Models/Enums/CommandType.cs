namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Command types.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// No command type.
        /// </summary>
        None = 0,

        /// <summary>
        /// A reply command declared in code.
        /// </summary>
        Reply,

        /// <summary>
        /// A reply command whose trigger text comes from a configuration file.
        /// </summary>
        ReplyDynamic,

        /// <summary>
        /// A slash command, for example /start.
        /// </summary>
        Slash,

        /// <summary>
        /// A step of a step-by-step command sequence.
        /// </summary>
        NextStep,

        /// <summary>
        /// An inline command triggered by a callbackQuery.
        /// </summary>
        Inline,

        /// <summary>
        /// A command handled through the message pipeline.
        /// </summary>
        Message,

        /// <summary>
        /// A command type defined by the application.
        /// </summary>
        Custom
    }
}
