using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Идентификаторы для callback команд
    /// </summary>
    public enum Header
    {
        [Description("Пустая команда")]
        None = 594,
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
        [Description("Бесплатный ВИП")]
        GetFreeVIP,
        [Description("Вип на 1 день")]
        GetVipOneDay,
        [Description("Вип на 1 неделю")]
        GetVipOneWeek,
        [Description("Вип на 1 месяц")]
        GetVipOneMonth,
        [Description("Вип навсегда")]
        GetVipOneForever,
        [Description("Пример 1")]
        ExampleOne,
        [Description("Пример 2")]
        ExampleTwo,
        [Description("Пример 3")]
        ExampleThree,
    }
}
