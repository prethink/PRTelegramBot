namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для работы с DI и экземплярами классов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BotHandlerAttribute : Attribute { }
}
