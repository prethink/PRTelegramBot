using PRTelegramBot.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Идентификаторы для callback команд
    /// </summary>
    [InlineCommand]
    public enum THeader
    {
        [Description("Пустая команда")]
        None = 0,
        [Description("Выбрать месяц")]
        PickMonth = 1,
        [Description("Выбрать год")]
        PickYear = 2,
        [Description("Изменение календаря")]
        ChangeTo = 3,
        [Description("Выбор года месяца")]
        YearMonthPicker = 4,
        [Description("Выбрать дату")]
        PickDate = 5,
        [Description("Следующая страница")]
        NextPage = 6,
        [Description("Текущая страница")]
        CurrentPage = 7 ,
        [Description("Предыдущая страница")]
        PreviousPage = 8,
    }
}
