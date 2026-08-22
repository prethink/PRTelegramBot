using System.ComponentModel;

namespace FastBotTemplateConsole.Models.Enums
{
    /// <summary>
    /// Message types for ads
    /// </summary>
    public enum MessageType
    {
        [Description("Text")]
        Text = 0,
        [Description("Photo")]
        Photo,
        [Description("Video")]
        Video,
        [Description("Document")]
        Document,
    }
}
