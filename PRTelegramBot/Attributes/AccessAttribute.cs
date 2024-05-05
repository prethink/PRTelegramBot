namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для проверки прав доступа на запуск методов.
    /// </summary>
    public class AccessAttribute : Attribute
    {
        public int Mask { get; private set; }

        public AccessAttribute(int mask)
        {
            Mask = mask;
        }
    }
}
