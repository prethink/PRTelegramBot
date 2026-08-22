namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Attribute for working with DI and class instances.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BotHandlerAttribute : Attribute { }
}
