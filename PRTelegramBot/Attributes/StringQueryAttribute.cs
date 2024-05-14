namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Общий атрибут для команд с типом string.
    /// </summary>
    public abstract class StringQueryAttribute : BaseQueryAttribute<string> 
    {
        #region Поля и свойства

        /// <summary>
        /// Тип сравнения команд.
        /// </summary>
        public Dictionary<string, StringComparison> CompareCommands { get; private set; } = new Dictionary<string, StringComparison>();

        #endregion

        #region Конструкторы

        public StringQueryAttribute(long botId) 
            : base(botId) { }

        #endregion
    }
}
