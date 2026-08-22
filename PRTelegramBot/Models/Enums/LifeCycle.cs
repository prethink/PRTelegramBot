namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Service lifetime used when bot handlers are registered in the DI container.
    /// </summary>
    public enum LifeCycle
    {
        /// <summary>
        /// One instance for the whole application.
        /// </summary>
        Singleton,

        /// <summary>
        /// One instance per scope — that is, per handled update.
        /// </summary>
        Scoped,

        /// <summary>
        /// A new instance on every resolve.
        /// </summary>
        Transient
    }
}
