using PRTelegramBot.Providers;

/// <summary>
/// Provider of the global settings.  
/// </summary>
public class PRSettingsProvider : IPRSettings
{
    #region Fields and properties

    /// <summary>
    /// Lazy initialization of the global settings instance.
    /// </summary>
    private static Lazy<IPRSettings> instance = new Lazy<IPRSettings>(() => new PRSettingsProvider());

    /// <summary>
    /// The global settings instance.
    /// </summary>
    public static IPRSettings Instance => instance.Value;

    #endregion

    #region Methods

    /// <summary>
    /// Sets a new global settings instance.
    /// </summary>
    /// <param name="settings"></param>
    public static void SetSettings(IPRSettings settings)
    {
        instance = new Lazy<IPRSettings>(() => settings);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.    
    /// </summary>
    private PRSettingsProvider() { }

    #endregion
}
