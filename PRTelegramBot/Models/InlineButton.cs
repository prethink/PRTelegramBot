using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models
{
    public class InlineButton : IInlineContent
    {
        private IBotContext context;

        private string buttonName;

        public object GetContent()
        {
            throw new NotImplementedException();
        }

        public string GetButtonName()
        {
            return buttonName;
        }

        /// <summary>
        /// Sets a new value for the button.
        /// </summary>
        /// <returns>Button name.</returns>
        public virtual string SetButtonName(string name)
        {
            buttonName = name;
            return buttonName;
        }

        public InlineButton(IBotContext context, string buttonName)
        {
            this.context = context;
            this.buttonName = buttonName;
        }
    }
}
