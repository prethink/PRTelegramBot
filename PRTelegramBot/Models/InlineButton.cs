using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Simple inline button that carries only a caption.
    /// </summary>
    public class InlineButton : IInlineContent
    {
        #region Fields and properties

        private IBotContext context;

        private string buttonName;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the content of the button.
        /// </summary>
        /// <returns>The button content.</returns>
        /// <exception cref="NotImplementedException">
        /// Always thrown: this button carries no payload, only a caption.
        /// </exception>
        public object GetContent()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the button caption.
        /// </summary>
        /// <returns>Button name.</returns>
        public string GetButtonName()
        {
            return buttonName;
        }

        /// <summary>
        /// Sets a new value for the button.
        /// </summary>
        /// <param name="name">New button caption.</param>
        /// <returns>Button name.</returns>
        public virtual string SetButtonName(string name)
        {
            buttonName = name;
            return buttonName;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="buttonName">Button caption.</param>
        public InlineButton(IBotContext context, string buttonName)
        {
            this.context = context;
            this.buttonName = buttonName;
        }

        #endregion
    }
}
