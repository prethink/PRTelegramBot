using System.ComponentModel;

namespace ConsoleExample.Models.Enums
{
    /// <summary>
    /// Типы меню для рекламы
    /// </summary>
    public enum MenuType
    {
        [Description("Без меню")]
        None = 0,
        [Description("Обычное кнопочное меню")]
        Reply,
        [Description("Inline меню")]
        Inline
    }
}
