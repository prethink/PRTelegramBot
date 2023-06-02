using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace PRTelegramBot.Configs
{
    class Settings
    {
        /// <summary>
        /// Уведомлять админинстратора о новом пользователе в боте
        /// </summary>
        public bool ShowNotifyNewUser { get; set; }

        /// <summary>
        /// Количество символов для персональной ссылки (Используется при создание купонов)
        /// </summary>
        public int WordLength { get; set; }
    } 
}
