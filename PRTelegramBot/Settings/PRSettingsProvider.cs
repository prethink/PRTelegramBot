using PRTelegramBot.Providers;

/// <summary>
/// Провайдер глобальных настроек.  
/// </summary>
public class PRSettingsProvider : IPRSettings
{
    #region Поля и свойства

    /// <summary>
    /// Lazy инициализация глобального экземпляра настроек.
    /// </summary>
    private static Lazy<IPRSettings> instance = new Lazy<IPRSettings>(() => new PRSettingsProvider());

    /// <summary>
    /// Глобальный экземпляр настроек.
    /// </summary>
    public static IPRSettings Instance => instance.Value;

    #endregion

    #region Методы

    /// <summary>
    /// Установить новый экземпляр глобальных настроек.
    /// </summary>
    /// <param name="settings"></param>
    public static void SetSettings(IPRSettings settings)
    {
        instance = new Lazy<IPRSettings>(() => settings);
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.    
    /// </summary>
    private PRSettingsProvider() { }

    #endregion
}
