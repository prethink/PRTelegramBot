namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Common interface for inline buttons.
    /// </summary>
    public interface IInlineContent
    {
        /// <summary>
        /// Gets the button name.
        /// </summary>
        /// <returns>Button name.</returns>
        public string GetButtonName();

        /// <summary>
        /// Sets a new value for the button.
        /// </summary>
        /// <returns>Button name.</returns>
        public string SetButtonName(string name);

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <returns>The button content.</returns>
        public object GetContent();
    }
}
