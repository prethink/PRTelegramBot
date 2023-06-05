using PRTelegramBot.Models.Interface;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Временные данные пользователя
    /// </summary>
    public class TelegramCache
    {
        /// <summary>
        /// Идентификатор сущности любого объекта
        /// </summary>
        public long? Id { get; set; }
        
        /// <summary>
        /// Ссылка на последнее сообщение
        /// </summary>
        public Message LastMessage { get; set; }


        //TODO: Добавить свои временные данные
        /// <summary>
        /// Очищает данные кеша
        /// </summary>
        public virtual void ClearData()
        {
            Id = null;
            LastMessage = null;
        }
    }
}
