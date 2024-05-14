namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы с DI и экземплярами классов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BotHandlerAttribute : Attribute { }
}
