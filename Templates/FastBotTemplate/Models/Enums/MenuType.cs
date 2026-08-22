using System.ComponentModel;

namespace FastBotTemplateConsole.Models.Enums
{
    /// <summary>
    /// Menu types for ads
    /// </summary>
    public enum MenuType
    {
        [Description("No menu")]
        None = 0,
        [Description("Regular button menu")]
        Reply,
        [Description("Inline menu")]
        Inline
    }
}
