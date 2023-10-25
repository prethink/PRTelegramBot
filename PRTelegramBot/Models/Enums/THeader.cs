using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Идентификаторы для callback команд
    /// </summary>
    public enum THeader
    {
        [Description("Пустая команда")]
        None = 0,
        [Description("Выбрать месяц")]
        PickMonth,
        [Description("Выбрать год")]
        PickYear,
        [Description("Изменение календаря")]
        ChangeTo,
        [Description("Выбор года месяца")]
        YearMonthPicker,
        [Description("Выбрать дату")]
        PickDate,
        [Description("Следующая страница")]
        NextPage,
        [Description("Текущая страница")]
        CurrentPage,
        [Description("Предыдущая страница")]
        PreviousPage,
    }
}
