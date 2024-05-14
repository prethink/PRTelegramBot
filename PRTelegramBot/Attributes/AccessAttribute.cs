namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для проверки прав доступа на запуск методов.
    /// </summary>
    public sealed class AccessAttribute : Attribute
    {
        #region Поля и свойства

        /// <summary>
        /// Маска доступа.
        /// </summary>
        public int Mask { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mask">Маска доступа.</param>
        public AccessAttribute(int mask)
        {
            Mask = mask;
        }

        #endregion
    }
}
